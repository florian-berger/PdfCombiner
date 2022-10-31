using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using DevExpress.Mvvm;
using PdfCombiner.Model;
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
        } private List<LanguageBinding> _availableLanguages;

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
        ///     Should the resources be optimized when merging?
        /// </summary>
        public bool OptimizeResources
        {
            get => _optimizeResources;
            set => SetProperty(ref _optimizeResources, value, () => OptimizeResources);
        } private bool _optimizeResources;

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
            App.Configuration.Save();

            Cancel();
        }

        private void Cancel()
        {
            WindowService?.GetWindow()?.Close();
        }

        private void ShowThirdPartyLicenses()
        {
            new ThirdPartyLicensesWindow(WindowService?.GetWindow()).ShowDialog();
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
