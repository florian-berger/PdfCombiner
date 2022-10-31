using System.Windows;

namespace PdfCombiner
{
    /// <summary>
    ///     Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        /// <summary>
        ///     Creates an instance of the window
        /// </summary>
        public SettingsWindow(Window owner = null)
        {
            Owner = owner ?? Application.Current.MainWindow;

            InitializeComponent();
        }
    }
}
