using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using PdfCombiner.Model;
using Xceed.Wpf.Toolkit.Core.Converters;

namespace PdfCombiner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Events

        /// <summary>
        ///     Event that is triggered when the configuration was changed
        /// </summary>
        public static event EventHandler ConfigurationChanged;

        #endregion Events

        #region Properties

        /// <summary>
        ///     Configuration of the app
        /// </summary>
        internal static CombinerConfig Configuration { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Constructor of the main app
        /// </summary>
        public App()
        {
#if DEBUG
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Environment.GetEnvironmentVariable("SyncFusion_20.3460.0.52_License", EnvironmentVariableTarget.User));
#else
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("$LICENSE_KEY$");
#endif
        }

        #endregion Constructor

        #region Overrides

        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Configuration = CombinerConfig.LoadConfig();

            var culture = new CultureInfo(Configuration.Language);

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }

        #endregion Overrides

        #region Internal methods

        /// <summary>
        ///     Raises the <see cref="ConfigurationChanged" /> event
        /// </summary>
        internal static void OnConfigurationChanged(object sender)
        {
            ConfigurationChanged?.Invoke(sender, EventArgs.Empty);
        }

        #endregion Internal methods
    }
}
