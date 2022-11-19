using System;
using System.Windows;

namespace PdfCombiner.Wpf.Controls
{
    /// <summary>
    ///     Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog
    {
        #region Private variables

        private readonly bool _isPasswordInput;

        #endregion Private variables

        #region Properties

        /// <summary>
        ///     Result of the dialog
        /// </summary>
        public InputDialogResult Result { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Private constructor
        /// </summary>
        private InputDialog(string title, string description, string defaultValue, bool isPasswordInput = false)
        {
            InitializeComponent();

            _isPasswordInput = isPasswordInput;
            if (isPasswordInput)
            {
                InputPasswordField.Password = defaultValue;
                InputField.Visibility = Visibility.Collapsed;
            }
            else
            {
                InputField.Text = defaultValue;
                InputPasswordField.Visibility = Visibility.Collapsed;
            }

            Title = title;
            DescriptionText.Text = description;

            Result = new InputDialogResult
            {
                IsCanceled = true
            };
        }

        #endregion Constructor

        #region Public methods

        /// <summary>
        ///     Shows the input dialog and returns the result
        /// </summary>
        /// <param name="owner">Owner window to place it centered</param>
        /// <param name="title">Title of the window</param>
        /// <param name="description">Description to display what the user should enter</param>
        /// <param name="defaultValue">Optional default value for the text field</param>
        public static InputDialogResult ShowDialog(Window owner, string title, string description, string defaultValue = "")
        {
            var dlg = new InputDialog(title, description, defaultValue)
            {
                Owner = owner
            };
            dlg.ShowDialog();

            return dlg.Result;
        }

        /// <summary>
        ///     Shows the password input dialog and returns the result
        /// </summary>
        /// <param name="owner">Owner window to place it centered</param>
        /// <param name="title">Title of the window</param>
        /// <param name="description">Description to display what the user should enter</param>
        /// <param name="defaultValue">Optional default value for the password field</param>
        public static InputDialogResult ShowPasswordDialog(Window owner, string title, string description, string defaultValue = "")
        {
            var dlg = new InputDialog(title, description, defaultValue, true)
            {
                Owner = owner
            };
            dlg.ShowDialog();

            return dlg.Result;
        }

        #endregion Public methods

        #region Private methods

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            Result = new InputDialogResult
            {
                IsCanceled = false,
                Value = _isPasswordInput ? InputPasswordField.Password : InputField.Text
            };

            Close();
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            if (_isPasswordInput)
            {
                InputPasswordField.SelectAll();
                InputPasswordField.Focus();
            }
            else
            {
                InputField.SelectAll();
                InputField.Focus();
            }
        }

        #endregion Private methods
    }

    /// <summary>
    ///     Result of the <see cref="InputDialog" />
    /// </summary>
    public class InputDialogResult
    {
        /// <summary>
        ///     Information if the dialog was canceled
        /// </summary>
        public bool IsCanceled { get; set; }

        /// <summary>
        ///     Value that was entered
        /// </summary>
        public string Value { get; set; }
    }
}
