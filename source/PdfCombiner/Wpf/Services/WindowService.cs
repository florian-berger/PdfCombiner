using DevExpress.Mvvm.UI;
using System.Windows;

namespace PdfCombiner.Wpf.Services
{
    /// <summary>
    ///     Interface that defines the methods for the window service
    /// </summary>
    internal interface IWindowService
    {
        /// <summary>
        ///     Returns the <see cref="Window" /> that is bound to the service 
        /// </summary>
        Window GetWindow();
    }

    /// <summary>
    ///     Service for making a <see cref="Window" /> accessible in a ViewModel
    /// </summary>
    internal class WindowService : ServiceBase, IWindowService
    {
        #region DependencyProperties

        /// <summary>
        ///     DependencyProperty to make <see cref="Window" /> bind-able
        /// </summary>
        public static readonly DependencyProperty WindowProperty = DependencyProperty.Register(
            nameof(Window), typeof(Window), typeof(WindowService), null);

        #endregion DependencyProperties

        #region Properties

        /// <summary>
        ///     Window that is bound to the service
        /// </summary>
        public Window Window
        {
            get => (Window)GetValue(WindowProperty);
            set => SetValue(WindowProperty, value);
        }

        #endregion Properties

        #region Public methods

        /// <inheritdoc />
        public Window GetWindow()
        {
            return Window;
        }

        #endregion Public methods
    }
}
