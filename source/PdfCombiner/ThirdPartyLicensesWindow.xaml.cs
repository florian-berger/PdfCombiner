using System.Windows;

namespace PdfCombiner
{
    /// <summary>
    /// Interaction logic for ThirdPartyLicensesWindow.xaml
    /// </summary>
    public partial class ThirdPartyLicensesWindow
    {
        /// <summary>
        ///     Creates an instance of the window
        /// </summary>
        public ThirdPartyLicensesWindow(Window owner = null)
        {
            Owner = owner ?? Application.Current.MainWindow;

            InitializeComponent();
        }
    }
}
