using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;
using PdfCombiner.Model;

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
                    LicenseText = "The MIT License (MIT)\r\n" +
                                  "\r\n" +
                                  "Copyright (c) 2000-2020 Developer Express Inc.\r\n" +
                                  "\r\n" +
                                  "Permission is hereby granted, free of charge, to any person obtaining a copy\r\n" +
                                  "of this software and associated documentation files (the \"Software\"), to deal\r\n" +
                                  "in the Software without restriction, including without limitation the rights\r\n" +
                                  "to use, copy, modify, merge, publish, distribute, sublicense, and/or sell\r\n" +
                                  "copies of the Software, and to permit persons to whom the Software is\r\n" +
                                  "furnished to do so, subject to the following conditions:\r\n" +
                                  "\r\n" +
                                  "The above copyright notice and this permission notice shall be included in all\r\n" +
                                  "copies or substantial portions of the Software.\r\n" +
                                  "\r\n" +
                                  "THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\r\n" +
                                  "IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\r\n" +
                                  "FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\r\n" +
                                  "AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\r\n" +
                                  "LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\r\n" +
                                  "OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE\r\n" +
                                  "SOFTWARE."
                },
                new ThirdPartyLicenseBinding
                {
                    Name = "Extended.Wpf.Toolkit",
                    Author = "Xceed",
                    Version = new Version(4, 4, 0),
                    Uri = "https://github.com/xceedsoftware/wpftoolkit",
                    LicenseType = "COMMUNITY LICENSE AGREEMENT",
                    LicenseText = "XCEED SOFTWARE, INC.\r\n" +
                                  "\r\n" +
                                  "COMMUNITY LICENSE AGREEMENT (for non-commercial use)\r\n" +
                                  "\r\n" +
                                  "This Community License Agreement (the “Agreement”) is a legal agreement between you (“Licensee”) and Xceed Software, Inc. (“Xceed”). Licensee wishes to use the “Xceed Extended WPF Toolkit™” (the “Software”), an Xceed product, for “Non-Commercial Use”. Such use of the Software means that it is not primarily intended for commercial advantages or for monetary compensation or any other type of compensation, including donations. Xceed agrees to license its products to developers like you as along as all terms & conditions set forth herein are respected. The Software is provided under a license; it is not “sold” in any manner. By installing, copying or otherwise using the Software, you confirm your agreement to the terms and conditions expressed in this Agreement. If you do not agree, you are not authorized to use our Software.\r\n" +
                                  "\r\n" +
                                  "GENERAL\r\n" +
                                  "\r\n" +
                                  "Subject to compliance with the conditions set out below, Xceed grants to Licensee a non-exclusive and perpetual right (unless/until revoked by Xceed at its discretion) to install and use the Software for designing, building, testing and/or deploying/distributing (to less than 10 users or end-users) an application, system or program for Non-Commercial Use only. Would Licensee need to use the Software for any purpose that is not strictly Non-Commercial Use, or if the Software is to be deployed or distributed to more than 10 users/end-users – even in a Non-Commercial Use, Licensee must acquire a Commercial License (with a paid subscription).\r\n" +
                                  "\r\n" +
                                  "The license granted under this Agreement is conditional on Licensee complying at all times with the following conditions:" +
                                  "\r\n" +
                                  "\r\n" +
                                  "-All of the Agreement’s terms & conditions are strictly complied with by the Licensee;\r\n" +
                                  "\r\n" +
                                  "-The Software is used for Non-Commercial Use only;\r\n" +
                                  "\r\n" +
                                  "-The Software cannot be resold, licensed, sublicensed or distributed by Licensee in any manner other than as specified above;\r\n" +
                                  "\r\n" +
                                  "-Xceed’s name and logo must appear clearly in the resulting work with an Xceed Copyright notice; the name and notice must be visible, not be hidden;\r\n" +
                                  "\r\n" +
                                  "-Licensee is not authorized to “deploy” the Software for/in any commercial environment;\r\n" +
                                  "\r\n" +
                                  "-Licensee commits not to create a competitive software product based on the Software;\r\n" +
                                  "\r\n" +
                                  "-Licensee is not authorized to sell or license/sub-license/lease the resulting work to anyone nor charge any amounts of money or accept donations or exchange services for the said resulting work.\r\n" +
                                  "\r\n" +
                                  "SUPPORT\r\n" +
                                  "\r\n" +
                                  "Support is not included in Community Licenses. The Software is provided on an “as is” basis only. Licensee can send requests to Xceed’s technical support team only if a commercial license has been obtained. Bugs may be corrected at Xceed’s discretion.\r\n" +
                                  "\r\n" +
                                  "WARRANTY\r\n" +
                                  "\r\n" +
                                  "Xceed clearly states that this Community License includes no warranty of any type. Xceed products are provided on an “as is” basis. In no case shall Xceed be responsible or liable for any direct or indirect, or consequential damages whatsoever (including, without limitation, any damages for loss of revenues, of business profits, business interruption, or loss of business information, or any other type of loss or damages) arising out of the use of the Software even if Xceed may have been advised of such potential damages or loss.\r\n" +
                                  "\r\n" +
                                  "OTHER\r\n" +
                                  "\r\n" +
                                  "Xceed does not allow Community Licensees to publish results from benchmarks or performance comparison tests (with other products) without advance permission by Xceed. Licensee is not authorized to use Xceed’s name, tradenames and trademarks without Xceed’s written permission (other than the Copyright notice stated above in the “General” section).\r\n" +
                                  "\r\n" +
                                  "GOVERNING LAW\r\n" +
                                  "\r\n" +
                                  "This Agreement shall be governed by the laws of the Province of Quebec (Canada). Any claim, dispute or problem arising out of this Agreement shall be deemed non-receivable or may be settled or disposed of at Xceed’s discretion. Xceed reserves the right to settle any action before an arbitration board in Quebec as per generally accepted, international rules of arbitration prevailing in Quebec.\r\n" +
                                  "\r\n" +
                                  "Xceed reserves the right to modify this Agreement at all times without notice.\r\n" +
                                  "\r\n" +
                                  "© Copyright: Xceed Software, Inc. - 2021. All rights reserved."
                },
                new ThirdPartyLicenseBinding
                {
                    Name = "FontAwesome.Sharp",
                    Author = "Awesome Incremented and Contributors",
                    Version = new Version(6, 1, 1),
                    Uri = "https://github.com/awesome-inc/FontAwesome.Sharp",
                    LicenseType = "Apache 2.0",
                    LicenseText = "   Copyright 2015-2022 Awesome Incremented and Contributors\r\n" +
                                  "\r\n" +
                                  "   Licensed under the Apache License, Version 2.0 (the \"License\");\r\n" +
                                  "   you may not use this file except in compliance with the License.\r\n" +
                                  "   You may obtain a copy of the License at\r\n" +
                                  "\r\n" +
                                  "       http://www.apache.org/licenses/LICENSE-2.0\r\n" +
                                  "\r\n" +
                                  "   Unless required by applicable law or agreed to in writing, software\r\n" +
                                  "   distributed under the License is distributed on an \"AS IS\" BASIS,\r\n" +
                                  "   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.\r\n" +
                                  "   See the License for the specific language governing permissions and\r\n" +
                                  "   limitations under the License."
                },
                new ThirdPartyLicenseBinding
                {
                    Name = "Syncfusion",
                    Author = "Syncfusion Inc.",
                    Version = new Version(20, 3, 0, 52),
                    Uri = "https://www.syncfusion.com/products/communitylicense",
                    LicenseType = "Community License",
                    LicenseText = "This software is covered by the Software License Agreement (the “Agreement”)(https://www.syncfusion.com/nuget/license?utm_source=nuget&utm_medium=listing). Any use in any form, regardless of where it was obtained is governed by the Agreement. The Agreement is a legal agreement between you (“You”, “Your”, or “Customer”) and Syncfusion, Inc., a Delaware corporation with its principal place of business located at 2501 Aerial Center Parkway, Suite 200, Morrisville, NC 27560 (“Syncfusion”). By accessing, downloading, viewing, possessing, or otherwise using any part of Syncfusion’s Essential Studio product, you are agreeing to be bound by the terms and conditions and agree to register with Syncfusion. If you do not agree to be bound by the terms and conditions of the Agreement, you cannot access, register, use, or view any part of Syncfusion’s Essential Studio product or lines of code.\r\n" +
                                  "\r\n" +
                                  "This license is for Essential Studio Enterprise Edition.\r\n" +
                                  "\r\n" +
                                  "TO READ THE COMPLETE LICENSE AGREEMENT: https://www.syncfusion.com/nuget/license?utm_source=nuget&utm_medium=listing,\r\n" +
                                  "\r\n" +
                                  "IF AFTER READING THIS AGREEMENT YOU HAVE ANY QUESTIONS ABOUT THIS AGREEMENT, PLEASE CONTACT SYNCFUSION PRIOR TO USING THE SOFTWARE PRODUCT VIA EMAIL AT LEGALQUESTIONS@SYNCFUSION.COM "
                }
            }.OrderBy(l => l.Name);
        }

        #endregion Private methods
    }
}
