using System.Diagnostics;
using DevExpress.Mvvm;

namespace PdfCombiner.Wpf
{
    /// <summary>
    ///     Class that holds a bunch of static commands that can be accessed over the whole project
    /// </summary>
    internal static class StaticCommands
    {
        #region Commands

        /// <summary>
        ///     Command to open a Uri
        /// </summary>
        public static DelegateCommand<string> OpenUriCommand => _openUriCommand ?? (_openUriCommand = new DelegateCommand<string>(OpenUrl));
        private static DelegateCommand<string> _openUriCommand;

        #endregion Commands

        #region Private methods

        private static void OpenUrl(string uri)
        {
            Process.Start(uri);
        }

        #endregion Private methods
    }
}
