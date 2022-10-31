using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PdfCombiner.Wpf.Converters
{
    /// <summary>
    ///     Converter to return <see cref="Visibility.Collapsed" /> when a string is empty
    /// </summary>
    internal class StringNotEmptyToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return string.IsNullOrWhiteSpace(stringValue) ? Visibility.Collapsed : Visibility.Visible;
            }

            throw new InvalidOperationException("Passed object is no string!");
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
