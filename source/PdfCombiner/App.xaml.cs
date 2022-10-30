using System;

namespace PdfCombiner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
#if DEBUG
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Environment.GetEnvironmentVariable("SyncFusion_20.3460.0.52_License", EnvironmentVariableTarget.User));
#else
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("$LICENSE_KEY$");
#endif
        }
    }
}
