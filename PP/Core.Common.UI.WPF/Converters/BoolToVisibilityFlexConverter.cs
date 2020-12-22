using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Core.Common.UI.WPF.Converters
{
    public class BoolToVisibilityFlexConverter : MarkupExtension, IValueConverter
    {
        private BoolToVisibilityFlexConverter converter;

        public BoolToVisibilityFlexConverter()
        {
        }

        public BoolToVisibilityFlexConverter(bool isInverse)
        {
            IsInverse = isInverse;
        }

        public bool IsInverse { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            converter = new BoolToVisibilityFlexConverter(IsInverse);
            return converter;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertValue = false;

            if (value != null)
            {
                if (value is bool b)
                {
                    convertValue = b;
                }
            }

            return (convertValue ^ IsInverse) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return ((visibility == Visibility.Visible) ^ IsInverse);
            }

            return false;
        }
    }
}
