using System;
using System.Globalization;
using System.Windows.Data;
using PdfCombiner.Helper;

namespace PdfCombiner.Wpf.Converters
{
    internal class GetReadableFileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long longValue)
            {
                return GetReadable.FileSize(longValue);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
