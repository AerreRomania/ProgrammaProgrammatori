using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core.Common.UI.WPF.Controls;

namespace Core.Common.UI.WPF.Converters
{
    public class XamlImageKeyToImageSourceConverter : MarkupExtension, IValueConverter
    {
        private static XamlImageKeyToImageSourceConverter converter;

        public XamlImageKeyToImageSourceConverter()
        {
        }

        public XamlImageKeyToImageSourceConverter(string foregroundResourceKey, double height, double width)
        {
            ForegroundResourceKey = foregroundResourceKey;
            Height = height;
            Width = width;
        }

        public double Height { get; set; } = 16;
        public double Width { get; set; } = 16;
        public string ForegroundResourceKey { get; set; } = "GlyphForeground";

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ?? (converter = new XamlImageKeyToImageSourceConverter(ForegroundResourceKey, Height, Width));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var keyParsed = Enum.TryParse(ForegroundResourceKey, out ThemeResourceKey foregroundThemeResourceKey);

            if (!(value is string key) || !keyParsed) return null;

            var element = new XamlImage
            {
                Template = (ControlTemplate)Application.Current.TryFindResource(key),
                Foreground = (Brush)Theme.GetResource(foregroundThemeResourceKey),
                Height = Height,
                Width = Width
            };

            element.Measure(new Size((int)element.Width, (int)element.Height));
            element.Arrange(new Rect(new Size((int)element.Width, (int)element.Height)));
            
            var dpiScale = VisualTreeHelper.GetDpi(element);

            var rtb = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, dpiScale.PixelsPerInchX, dpiScale.PixelsPerInchY, PixelFormats.Pbgra32);

            rtb.Render(element);

            return rtb;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
