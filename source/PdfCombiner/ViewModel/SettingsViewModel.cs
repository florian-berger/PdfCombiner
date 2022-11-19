using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DevExpress.Mvvm;
using PdfCombiner.Model;
using Syncfusion.Pdf.Security;
using IWindowService = PdfCombiner.Wpf.Services.IWindowService;

namespace PdfCombiner.ViewModel
{
    /// <summary>
    ///     ViewModel for the programs settings
    /// </summary>
    internal class SettingsViewModel : ViewModelBase
    {
        #region Private variables

        private readonly string _currentLanguage;

        #endregion Private variables

        #region Properties

        /// <summary>
        ///     Is the 'General' tab selected?
        /// </summary>
        public bool IsGeneralSelected
        {
            get => _isGeneralSelected;
            set => SetProperty(ref _isGeneralSelected, value, () => IsGeneralSelected);
        } private bool _isGeneralSelected;

        /// <summary>
        ///     Is the 'Security' tab selected?
        /// </summary>
        public bool IsSecuritySelected
        {
            get => _isSecuritySelected;
            set => SetProperty(ref _isSecuritySelected, value, () => IsSecuritySelected);
        } private bool _isSecuritySelected;

        /// <summary>
        ///     Is the 'Compression' tab selected?
        /// </summary>
        public bool IsCompressionSelected
        {
            get => _isCompressionSelected;
            set => SetProperty(ref _isCompressionSelected, value, () => IsCompressionSelected);
        } private bool _isCompressionSelected;

        /// <summary>
        ///     Is the 'About' tab selected?
        /// </summary>
        public bool IsAboutSelected
        {
            get => _isAboutSelected;
            set => SetProperty(ref _isAboutSelected, value, () => IsAboutSelected);
        } private bool _isAboutSelected;

        /// <summary>
        ///     List of available languages
        /// </summary>
        public List<LanguageBinding> AvailableLanguages
        {
            get
            {
                if (_availableLanguages != null)
                {
                    return _availableLanguages;
                }

                return _availableLanguages = CombinerConfig.SupportedLanguages
                    .Select(l => new LanguageBinding(l, new CultureInfo(l).DisplayName)).ToList();
            }
        } private static List<LanguageBinding> _availableLanguages;

        /// <summary>
        ///     List of available encryption algorithms
        /// </summary>
        public List<EncryptionAlgorithmBinding> AvailableEncryptionAlgorithms
        {
            get
            {
                if (_availableEncryptionAlgorithms != null)
                {
                    return _availableEncryptionAlgorithms;
                }

                return _availableEncryptionAlgorithms = Enum.GetValues(typeof(PdfEncryptionAlgorithm)).Cast<PdfEncryptionAlgorithm>()
                    .Select(a => new EncryptionAlgorithmBinding(a)).ToList();
            }
        } private static List<EncryptionAlgorithmBinding> _availableEncryptionAlgorithms;

        /// <summary>
        ///     List of available encryption key sizes
        /// </summary>
        public List<EncryptionKeySizeBinding> AvailableEncryptionKeySizes
        {
            get
            {
                if (_availableEncryptionKeySizes != null)
                {
                    return _availableEncryptionKeySizes;
                }

                return _availableEncryptionKeySizes = Enum.GetValues(typeof(PdfEncryptionKeySize)).Cast<PdfEncryptionKeySize>()
                    .Select(a => new EncryptionKeySizeBinding(a)).ToList();
            }
        } private static List<EncryptionKeySizeBinding> _availableEncryptionKeySizes;

        /// <summary>
        ///     Language that should be used
        /// </summary>
        public LanguageBinding SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                SetProperty(ref _selectedLanguage, value, () => SelectedLanguage);
                RaisePropertyChanged(() => ShowLanguageChangedHint);
            }
        } private LanguageBinding _selectedLanguage;

        /// <summary>
        ///     Algorithm that is used for encryption
        /// </summary>
        public EncryptionAlgorithmBinding SelectedEncryptionAlgorithm
        {
            get => _selectedEncryptionAlgorithm;
            set => SetProperty(ref _selectedEncryptionAlgorithm, value, () => SelectedEncryptionAlgorithm);
        } private EncryptionAlgorithmBinding _selectedEncryptionAlgorithm;

        /// <summary>
        ///     Selected key size for encryption
        /// </summary>
        public EncryptionKeySizeBinding SelectedKeySizeBinding
        {
            get => _selectedKeySizeBinding;
            set => SetProperty(ref _selectedKeySizeBinding, value, () => SelectedKeySizeBinding);
        } private EncryptionKeySizeBinding _selectedKeySizeBinding;

        /// <summary>
        ///     Should the resources be optimized when merging?
        /// </summary>
        public bool OptimizeResources
        {
            get => _optimizeResources;
            set => SetProperty(ref _optimizeResources, value, () => OptimizeResources);
        } private bool _optimizeResources;

        /// <summary>
        ///     Should result documents be encrypted with a password?
        /// </summary>
        public bool EncryptDocuments
        {
            get => _encryptDocuments;
            set => SetProperty(ref _encryptDocuments, value, () => EncryptDocuments);
        } private bool _encryptDocuments;

        /// <summary>
        ///     Should images be compressed?
        /// </summary>
        public bool CompressImages
        {
            get => _compressImages;
            set => SetProperty(ref _compressImages, value, () => CompressImages);
        } private bool _compressImages;

        /// <summary>
        ///     Quality a compressed image should have (100 percent is best)
        /// </summary>
        public double ImageQuality
        {
            get => _imageQuality;
            set => SetProperty(ref _imageQuality, value, () => ImageQuality);
        } private double _imageQuality;

        /// <summary>
        ///     Should fonts be optimized?
        /// </summary>
        public bool OptimizeFonts
        {
            get => _optimizeFonts;
            set => SetProperty(ref _optimizeFonts, value, () => OptimizeFonts);
        } private bool _optimizeFonts;

        /// <summary>
        ///     Should page contents be optimized?
        /// </summary>
        public bool OptimizeContents
        {
            get => _optimizeContents;
            set => SetProperty(ref _optimizeContents, value, () => OptimizeContents);
        } private bool _optimizeContents;

        /// <summary>
        ///     Returns of the current version of the software
        /// </summary>
        public string CurrentVersion
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_currentVersion))
                {
                    return _currentVersion;
                }

                if (Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true)
                        .FirstOrDefault() is AssemblyFileVersionAttribute versionAttribute)
                {
                    _currentVersion = versionAttribute.Version;
                }

                return _currentVersion ?? (_currentVersion = "<unknown>");
            }
        } private string _currentVersion;

        /// <summary>
        ///     Information whether a hint about restarting the software should be displayed
        /// </summary>
        public bool ShowLanguageChangedHint => _currentLanguage != SelectedLanguage.Identifier;

        #endregion Properties

        #region Constructor

        public SettingsViewModel()
        {
            _currentLanguage = CultureInfo.DefaultThreadCurrentCulture.IetfLanguageTag;

            _selectedLanguage = AvailableLanguages.FirstOrDefault(l => l.Identifier == App.Configuration.Language);
            _optimizeResources = App.Configuration.OptimizeResourcesWhileMerging;
            _encryptDocuments = App.Configuration.EncryptDocuments;

            _selectedEncryptionAlgorithm = AvailableEncryptionAlgorithms.FirstOrDefault(a => a.Algorithm.ToString() == (App.Configuration.EncryptionAlgorithm ?? PdfEncryptionAlgorithm.AES.ToString()));
            _selectedKeySizeBinding = AvailableEncryptionKeySizes.FirstOrDefault(ks => ks.KeySize.ToString() == (App.Configuration.EncryptionKeySize ?? PdfEncryptionKeySize.Key128Bit.ToString()));

            _compressImages = App.Configuration.CompressImages;
            _imageQuality = App.Configuration.ImageQuality;
            _optimizeFonts = App.Configuration.OptimizeFonts;
            _optimizeContents = App.Configuration.OptimizePageContents;

            //new PdfCompressionOptions
            //{
            //    CompressImages = true,
            //    ImageQuality = 100,
            //    OptimizeFont = true,
            //    OptimizePageContents = true
            //};

            IsGeneralSelected = true;
        }

        #endregion Constructor

        #region Commands

        /// <summary>
        ///     Command to save the settings
        /// </summary>
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
        private DelegateCommand _saveCommand;

        /// <summary>
        ///     Command to cancel the editing
        /// </summary>
        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        private DelegateCommand _cancelCommand;

        /// <summary>
        ///     Command to display the third party licenses used by this software
        /// </summary>
        public DelegateCommand ShowThirdPartyLicensesCommand => _showThirdPartyLicensesCommand ?? (_showThirdPartyLicensesCommand = new DelegateCommand(ShowThirdPartyLicenses));
        private DelegateCommand _showThirdPartyLicensesCommand;

        #endregion Commands

        #region Private methods

        private void Save()
        {
            App.Configuration.Language = SelectedLanguage.Identifier;
            App.Configuration.OptimizeResourcesWhileMerging = OptimizeResources;
            App.Configuration.EncryptDocuments = _encryptDocuments;
            App.Configuration.EncryptionAlgorithm = SelectedEncryptionAlgorithm?.Algorithm.ToString() ?? PdfEncryptionAlgorithm.AES.ToString();
            App.Configuration.EncryptionKeySize = SelectedKeySizeBinding?.KeySize.ToString() ?? PdfEncryptionKeySize.Key128Bit.ToString();
            App.Configuration.CompressImages = _compressImages;
            App.Configuration.ImageQuality = _imageQuality;
            App.Configuration.OptimizeFonts = _optimizeFonts;
            App.Configuration.OptimizePageContents = _optimizeContents;
            App.Configuration.Save();

            App.OnConfigurationChanged(this);
            CloseWindow();
        }

        private void Cancel()
        {
            CloseWindow();
        }

        private void ShowThirdPartyLicenses()
        {
            new ThirdPartyLicensesWindow(WindowService?.GetWindow()).ShowDialog();
        }

        private void CloseWindow()
        {
            WindowService?.GetWindow()?.Close();
        }

        #endregion Private methods

        #region Services

        /// <summary>
        ///     Service to access the window object
        /// </summary>
        internal IWindowService WindowService => GetService<IWindowService>();

        #endregion Services
    }
}
