using System;
using System.Globalization;
using System.Windows.Data;
using PdfCombiner.Resources.Language;

namespace PdfCombiner.Wpf.Converters
{
    internal class BoolYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? GlobalResource.Yes : GlobalResource.No;
            }

            throw new ArgumentException("Input must be a boolean!");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
