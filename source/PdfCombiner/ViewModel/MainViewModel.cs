using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using Microsoft.Win32;
using PdfCombiner.Helper;
using PdfCombiner.Model;
using PdfCombiner.Resources.Language;
using PdfCombiner.Wpf;
using PdfCombiner.Wpf.Services;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Security;
using Syncfusion.UI.Xaml.Grid;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace PdfCombiner.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        #region Private variables

        private bool _loadedHandled;

        #endregion Private variables

        #region Properties

        /// <summary>
        ///     Collection of all <see cref="FileBinding" />s that should be combined
        /// </summary>
        public AddRangeObservableCollection<FileBinding> Files
        {
            get => _files;
            set => SetProperty(ref _files, value, () => Files);
        } private AddRangeObservableCollection<FileBinding> _files;

        /// <summary>
        ///     List of all selected <see cref="FileBinding" />
        /// </summary>
        public AddRangeObservableCollection<FileBinding> SelectedFiles
        {
            get => _selectedFiles;
            set => SetProperty(ref _selectedFiles, value, () => SelectedFiles);
        } private AddRangeObservableCollection<FileBinding> _selectedFiles;

        /// <summary>
        ///     Information if there program is working
        /// </summary>
        public bool IsWorking
        {
            get => _isWorking;
            set => SetProperty(ref _isWorking, value, () => IsWorking);
        } private bool _isWorking;

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Initializes the <see cref="Files" /> collection
        /// </summary>
        public MainViewModel()
        {
            Files = new AddRangeObservableCollection<FileBinding>();
            SelectedFiles = new AddRangeObservableCollection<FileBinding>();

            App.ConfigurationChanged += AppConfigurationChanged;
            RefreshConfigurationDisplay();
        }

        #endregion Constructor

        #region Commands

        /// <summary>
        ///     Command that is triggered when the view is loaded
        /// </summary>
        public DelegateCommand ViewLoadedCommand => _viewLoadedCommand ?? (_viewLoadedCommand = new DelegateCommand(ViewLoaded));
        private DelegateCommand _viewLoadedCommand;

        public DelegateCommand FilesGridSelectionChangedCommand => _filesGridSelectionChangedCommand ?? (_filesGridSelectionChangedCommand = new DelegateCommand(FilesGridSelectionChanged));
        private DelegateCommand _filesGridSelectionChangedCommand;

        /// <summary>
        ///     Command to start the PDF combination
        /// </summary>
        public AsyncCommand CombinePdfFilesCommand => _combinePdfFilesCommand ?? (_combinePdfFilesCommand = new AsyncCommand(CombinePdfFiles, CanCombinePdfFiles, false, true));
        private AsyncCommand _combinePdfFilesCommand;

        /// <summary>
        ///     Command to add files to the combiner
        /// </summary>
        public AsyncCommand AddFileCommand => _addFileCommand ?? (_addFileCommand = new AsyncCommand(AddFile, true));
        private AsyncCommand _addFileCommand;

        /// <summary>
        ///     Command to remove the selected files from the combiner
        /// </summary>
        public DelegateCommand RemoveSelectedFilesCommand => _removeSelectedFilesCommand ?? (_removeSelectedFilesCommand = new DelegateCommand(RemoveSelectedFiles, CanRemoveSelectedFiles, true));
        private DelegateCommand _removeSelectedFilesCommand;

        /// <summary>
        ///     Clears the list of all added files
        /// </summary>
        public DelegateCommand RemoveAllFilesCommand => _removeAllFilesCommand ?? (_removeAllFilesCommand = new DelegateCommand(RemoveAllFiles, CanRemoveAllFiles, true));
        private DelegateCommand _removeAllFilesCommand;

        /// <summary>
        ///     Command to show the programs settings
        /// </summary>
        public DelegateCommand SettingsCommand => _settingsCommand ?? (_settingsCommand = new DelegateCommand(Settings));
        private DelegateCommand _settingsCommand;

        /// <summary>
        ///     Command to open the GitHub repository of this project
        /// </summary>
        public DelegateCommand OpenGithubRepoCommand => _openGithubRepoCommand ?? (_openGithubRepoCommand = new DelegateCommand(OpenGithubRepo));
        private DelegateCommand _openGithubRepoCommand;

        /// <summary>
        ///     Command to display the third party licenses used by this software
        /// </summary>
        public DelegateCommand ShowThirdPartyLicensesCommand => _showThirdPartyLicensesCommand ?? (_showThirdPartyLicensesCommand = new DelegateCommand(ShowThirdPartyLicenses));
        private DelegateCommand _showThirdPartyLicensesCommand;

        #endregion Commands

        #region Private methods

        private void ViewLoaded()
        {
            if (_loadedHandled)
            {
                return;
            }

            var dragDropController = GridService?.GetGrid()?.RowDragDropController;
            if (dragDropController != null)
            {
                _loadedHandled = true;
                dragDropController.Dropped += SortItemSource;
            }
        }

        private void FilesGridSelectionChanged()
        {
            var grid = GridService?.GetGrid();
            if (grid == null)
            {
                return;
            }
            
            SelectedFiles.SetRange(grid.SelectedItems.Cast<FileBinding>().Where(f => f != null));
        }

        private bool CanCombinePdfFiles()
        {
            // Combining is allowed, when:
            return
                Files?.Any() == true && // There is at least one file defined,
                !IsWorking; // And it is currently not working
        }

        private bool CanRemoveSelectedFiles()
        {
            return SelectedFiles?.Any() == true && !IsWorking;
        }

        private bool CanRemoveAllFiles()
        {
            return Files?.Any() == true && !IsWorking;
        }

        private async Task CombinePdfFiles()
        {
            if (!CanCombinePdfFiles())
            {
                return;
            }

            if (Files.Any(f => !f.IsValidPdfFile))
            {
                MessageBox.Show(Application.Current.MainWindow, MainWindowResource.InvalidFilesExisting,
                    MainWindowResource.CannotCombineTitle, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var targetFileDlg = new SaveFileDialog
            {
                Filter = MainWindowResource.PdfFilter,
                Title = MainWindowResource.SelectTargetFile,
                ValidateNames = true,
                OverwritePrompt = true,
                AddExtension = true
            };

            if (targetFileDlg.ShowDialog(Application.Current.MainWindow) != true)
            {
                return;
            }

            var targetFile = targetFileDlg.FileName;

            IsWorking = true;
            try
            {
                await Task.Run(() =>
                    ExecuteCombination(targetFile, Files.Select(f => f.FileLocation).ToArray()));
            }
            catch (Exception ex)
            {
                IsWorking = false;
                MessageBox.Show(Application.Current.MainWindow,
                    $"{MainWindowResource.ErrorCombiningDescription}{Environment.NewLine}{ex.Message}",
                    MainWindowResource.ErrorCombiningTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IsWorking = false;
            var result = MessageBox.Show(Application.Current.MainWindow,
                MainWindowResource.SuccessfullyMergedDescription, MainWindowResource.SuccessfullyMergedTitle,
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Process.Start(targetFile);
                }
                catch
                {
                    // Error opening the PDF - Ignore
                }
            }
        }
        
        private async Task AddFile()
        {
            var fileDlg = new OpenFileDialog
            {
                Title = MainWindowResource.SelectFiles,
                Filter = MainWindowResource.AddFilesFilter,
                Multiselect = true
            };

            if (!fileDlg.ShowDialog(Application.Current.MainWindow) == true)
            {
                return;
            }

            if (fileDlg.FileNames.Length < 1)
            {
                return;
            }

            foreach (var fileName in fileDlg.FileNames)
            {
                var file = await FileBinding.Create(fileName);
                Files.Add(file);
            }
        }

        private void RemoveSelectedFiles()
        {
            if (!CanRemoveSelectedFiles())
            {
                return;
            }

            var cleanedFiles = Files.ToList();
            foreach (var file in SelectedFiles)
            {
                cleanedFiles.Remove(file);
            }

            Files.SetRange(cleanedFiles);
            SelectedFiles.Clear();
        }

        private void RemoveAllFiles()
        {
            if (!CanRemoveAllFiles())
            {
                return;
            }

            Files.Clear();
            SelectedFiles.Clear();
        }

        private void ExecuteCombination(string target, string[] files)
        {
            var finalDoc = new PdfDocument();

            var mergeOptions = new PdfMergeOptions
            {
                OptimizeResources = App.Configuration.OptimizeResourcesWhileMerging
            };

            PdfDocumentBase.Merge(finalDoc, mergeOptions, files.ToArray<object>());

            finalDoc.Save(target);
            finalDoc.Close(true);
        }

        private void Settings()
        {
            new SettingsWindow().ShowDialog();
        }

        private void OpenGithubRepo()
        {
            Process.Start(CombinerConstants.RepositoryUri);
        }

        private void ShowThirdPartyLicenses()
        {
            new ThirdPartyLicensesWindow().ShowDialog();
        }

        private void SortItemSource(object sender, GridRowDroppedEventArgs e)
        {
            var grid = GridService?.GetGrid();
            var orderedFiles = grid?.View?.Records?.Select(s => s.Data as FileBinding).Where(f => f != null).ToList();
            if (orderedFiles == null || !orderedFiles.Any())
            {
                return;
            }

            Files.SetRange(orderedFiles);
        }

        private void AppConfigurationChanged(object sender, EventArgs e)
        {
            RefreshConfigurationDisplay();
        }

        private void RefreshConfigurationDisplay()
        {
            RaisePropertiesChanged(nameof(IsEncryptionActive), nameof(EncryptionDetails),
                nameof(IsCompressionActive), nameof(CompressionDetails));
        }

        /// <summary>
        ///     Information if encryption is active
        /// </summary>
        public bool IsEncryptionActive => App.Configuration.EncryptDocuments;

        /// <summary>
        ///     Details about encryption
        /// </summary>
        public string EncryptionDetails
        {
            get
            {
                var encryptionAlgorithm =
                    Enum.TryParse(App.Configuration.EncryptionAlgorithm, out PdfEncryptionAlgorithm algorithm)
                        ? EncryptionAlgorithmBinding.GetTranslatedName(algorithm)
                        : GlobalResource.Error.ToUpper();

                var encryptionKeySize = Enum.TryParse(App.Configuration.EncryptionKeySize, out PdfEncryptionKeySize keySize)
                    ? EncryptionKeySizeBinding.GetTranslatedName(keySize)
                    : GlobalResource.Error.ToUpper();

                return $" - {MainWindowResource.EncryptionAlgorithm}: {encryptionAlgorithm}{Environment.NewLine} - {MainWindowResource.EncryptionKeySize}: {encryptionKeySize}";
            }
        }

        /// <summary>
        ///     Information if compression is active
        /// </summary>
        public bool IsCompressionActive => App.Configuration.CompressImages || App.Configuration.OptimizePageContents ||
                                           App.Configuration.OptimizeFonts || App.Configuration.OptimizeResourcesWhileMerging;

        /// <summary>
        ///     Details about the compression
        /// </summary>
        public string CompressionDetails
        {
            get
            {
                var imageCompression = $" - {MainWindowResource.CompressionImages}: {(App.Configuration.CompressImages ? GlobalResource.Yes : GlobalResource.No)}";
                if (App.Configuration.CompressImages)
                {
                    imageCompression += $" ({App.Configuration.ImageQuality} %)";
                }

                var sb = new StringBuilder();
                sb.AppendLine($" - {MainWindowResource.CompressionResources}: {(App.Configuration.OptimizeResourcesWhileMerging ? GlobalResource.Yes : GlobalResource.No)}");
                sb.AppendLine(imageCompression);
                sb.AppendLine($" - {MainWindowResource.CompressionPageContents}: {(App.Configuration.OptimizePageContents ? GlobalResource.Yes : GlobalResource.No)}");
                sb.AppendLine($" - {MainWindowResource.CompressionFonts}: {(App.Configuration.OptimizeFonts ? GlobalResource.Yes : GlobalResource.No)}");
                return sb.ToString();
            }
        }

        #endregion Private methods

        #region Services

        internal ISfDataGridService GridService => GetService<ISfDataGridService>();

        #endregion Services
    }
}
