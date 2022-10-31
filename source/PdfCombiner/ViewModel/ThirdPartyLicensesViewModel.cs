using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DevExpress.Mvvm;
using PdfCombiner.Model;
#if !DEBUG
using PdfCombiner.Resources.Language;
#endif

namespace PdfCombiner.ViewModel
{
    /// <summary>
    ///     ViewModel containing all Third-party licenses
    /// </summary>
    internal class ThirdPartyLicensesViewModel : ViewModelBase
    {
        #region Properties

        /// <summary>
        ///     All licenses that are used
        /// </summary>
        public IEnumerable<ThirdPartyLicenseBinding> Licenses => _licenses ?? (_licenses = BuildLicenses());
        private static IEnumerable<ThirdPartyLicenseBinding> _licenses;

        /// <summary>
        ///     Selected license object
        /// </summary>
        public ThirdPartyLicenseBinding SelectedLicense
        {
            get => _selectedLicense;
            set => SetProperty(ref _selectedLicense, value, () => SelectedLicense);
        } private ThirdPartyLicenseBinding _selectedLicense;

        #endregion Properties

        #region Private methods

        private IEnumerable<ThirdPartyLicenseBinding> BuildLicenses()
        {
            return new List<ThirdPartyLicenseBinding>
            {
                new ThirdPartyLicenseBinding
                {
                    Name = "DevExpressMvvm",
                    Author = "Developer Express Inc.",
                    Version = new Version(22, 1, 3),
                    Uri = "https://github.com/DevExpress/DevExpress.Mvvm.Free",
                    LicenseType = "MIT",
                    LicenseText = GetLicenseTextFromResources("DevExpressMvvm")
                },
                new ThirdPartyLicenseBinding
                {
                    Name = "Extended.Wpf.Toolkit",
                    Author = "Xceed",
                    Version = new Version(4, 4, 0),
                    Uri = "https://github.com/xceedsoftware/wpftoolkit",
                    LicenseType = "COMMUNITY LICENSE AGREEMENT",
                    LicenseText = GetLicenseTextFromResources("Extended.Wpf.Toolkit")
                },
                new ThirdPartyLicenseBinding
                {
                    Name = "FontAwesome.Sharp",
                    Author = "Awesome Incremented and Contributors",
                    Version = new Version(6, 1, 1),
                    Uri = "https://github.com/awesome-inc/FontAwesome.Sharp",
                    LicenseType = "Apache 2.0",
                    LicenseText = GetLicenseTextFromResources("FontAwesome.Sharp")
                },
                new ThirdPartyLicenseBinding
                {
                    Name = "Syncfusion",
                    Author = "Syncfusion Inc.",
                    Version = new Version(20, 3, 0, 52),
                    Uri = "https://www.syncfusion.com/products/communitylicense",
                    LicenseType = "Community License",
                    LicenseText = GetLicenseTextFromResources("Syncfusion")
                },
                new ThirdPartyLicenseBinding
                {
                    Name = "Newtonsoft.Json",
                    Author = "James Newton-King",
                    Version = new Version(13, 0, 1),
                    Uri = "https://www.newtonsoft.com/json",
                    LicenseType = "MIT",
                    LicenseText = GetLicenseTextFromResources("Newtonsoft.Json")
                }
            }.OrderBy(l => l.Name);
        }

        private string GetLicenseTextFromResources(string name)
        {
            var resName = $"PdfCombiner.Resources.Licenses.{name}.txt";

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resName))
            {
                if (stream != null)
                {
                    using (var sr = new StreamReader(stream))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }

#if DEBUG
            throw new InvalidOperationException($"Missing license file for {name}!");
#else
            return ThirdPartyLicensesResource.NoLicenseFileFound;
#endif
        }

        #endregion Private methods
    }
}
