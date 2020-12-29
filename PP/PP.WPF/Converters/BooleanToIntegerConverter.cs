using System;
using System.Globalization;
using System.Windows.Data;

namespace PP.WPF.Converters
{
    public class BooleanToIntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (bool)value == false ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || (int)value != 0;
        }
    }
}