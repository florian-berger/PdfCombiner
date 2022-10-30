using System.Windows;
using DevExpress.Mvvm.UI;
using Syncfusion.UI.Xaml.Grid;

namespace PdfCombiner.Wpf.Services
{
    internal interface ISfDataGridService
    {
        /// <summary>
        ///     Returns the grid bind to the service
        /// </summary>
        SfDataGrid GetGrid();
    }

    internal class SfDataGridService : ServiceBase, ISfDataGridService
    {
        #region DependencyProperties

        /// <summary>
        ///     Property to make <see cref="Grid" /> bind-able
        /// </summary>
        public static readonly DependencyProperty GridProperty = DependencyProperty.Register(
            nameof(Grid), typeof(SfDataGrid), typeof(SfDataGridService), null);

        #endregion DependencyProperties

        #region Properties

        /// <summary>
        ///     Grid that is bound to the service
        /// </summary>
        public SfDataGrid Grid
        {
            get => (SfDataGrid) GetValue(GridProperty);
            set => SetValue(GridProperty, value);
        }

        #endregion Properties

        #region ISfDataGridService

        /// <inheritdoc />
        public SfDataGrid GetGrid()
        {
            return Grid;
        }

        #endregion ISfDataGridService
    }
}
