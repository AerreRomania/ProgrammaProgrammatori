using System;
using System.Windows;
using System.Windows.Media;

namespace Core.Common.UI.WPF
{
    public sealed class Theme
    {
        [ThreadStatic]
        private static ResourceDictionary _resourceDictionary;

        internal static ResourceDictionary ResourceDictionary
        {
            get
            {
                if (_resourceDictionary != null)
                {
                    return _resourceDictionary;
                }

                _resourceDictionary = new ResourceDictionary();
                LoadThemeType(ThemeType.Light);
                return _resourceDictionary;
            }
        }

        public static ThemeType ThemeType { get; set; } = ThemeType.Light;

        public static void LoadThemeType(ThemeType type)
        {
            ThemeType = type;

            SetResource(ThemeResourceKey.ControlDisabledOpacity.ToString(), 0.4d);
            SetResource(ThemeResourceKey.ControlDefaultBorder.ToString(), new SolidColorBrush(ColorFromHex("#FFC12B68")));
            SetResource(ThemeResourceKey.ControlMouseOverBorder.ToString(), new SolidColorBrush(ColorFromHex("#FFA53289")));
            SetResource(ThemeResourceKey.ControlFocusBorder.ToString(), new SolidColorBrush(ColorFromHex("#FF833AB4")));
            SetResource(ThemeResourceKey.ListMouseOverBackground.ToString(), new SolidColorBrush(ColorFromHex("#22833AB4")));
            SetResource(ThemeResourceKey.ListMouseOverBorder.ToString(), new SolidColorBrush(ColorFromHex("#FFA53289")));
            SetResource(ThemeResourceKey.ListSelectedBackground.ToString(), new SolidColorBrush(ColorFromHex("#FF833AB4")));
            SetResource(ThemeResourceKey.ListSelectedBorder.ToString(), new SolidColorBrush(ColorFromHex("#FFA53289")));
            SetResource(ThemeResourceKey.ListSelectedForeground.ToString(), new SolidColorBrush(ColorFromHex("#FFFFFFFF")));
            SetResource(ThemeResourceKey.WindowBorder.ToString(), new SolidColorBrush(ColorFromHex("#FF616D7B")));
            SetResource(ThemeResourceKey.WindowActiveBorder.ToString(), new SolidColorBrush(ColorFromHex("#FF833AB4")));
            SetResource(ThemeResourceKey.WindowControlMouseOverBackground.ToString(), new SolidColorBrush(ColorFromHex("#FF000000")));
            SetResource(ThemeResourceKey.WindowHeaderBackground.ToString(), GetWindowHeaderGradient());
            SetResource(ThemeResourceKey.WindowHeaderForeground.ToString(), new SolidColorBrush(ColorFromHex("#FFFFFFFF")));

            switch (type)
            {
                case ThemeType.Light:
                    {
                        SetResource(ThemeResourceKey.ContentBackground.ToString(), new SolidColorBrush(ColorFromHex("#F9F9F9")));
                        SetResource(ThemeResourceKey.ContentForeground.ToString(), new SolidColorBrush(ColorFromHex("#FF3F3F3F")));
                        SetResource(ThemeResourceKey.ControlForeground.ToString(), new SolidColorBrush(ColorFromHex("#FF3F3F3F")));
                        SetResource(ThemeResourceKey.ControlBackground.ToString(), new SolidColorBrush(ColorFromHex("#FFDBE0E4")));
                        SetResource(ThemeResourceKey.ControlBorder.ToString(), new SolidColorBrush(ColorFromHex("#FF8192A1")));
                        SetResource(ThemeResourceKey.ControlContentBackground.ToString(), new SolidColorBrush(ColorFromHex("#FFFFFFFF")));
                        SetResource(ThemeResourceKey.ControlHighlightBackground.ToString(), new SolidColorBrush(ColorFromHex("#77833AB4")));
                        SetResource(ThemeResourceKey.ControlMouseOverBackground.ToString(), new SolidColorBrush(ColorFromHex("#FFEDEFF2")));
                        SetResource(ThemeResourceKey.ControlPressedBackground.ToString(), new SolidColorBrush(ColorFromHex("#FFBFC2C4")));
                        SetResource(ThemeResourceKey.ControlPressedBorder.ToString(), new SolidColorBrush(ColorFromHex("#FF3CC0FF")));
                        SetResource(ThemeResourceKey.GlyphForeground.ToString(), new SolidColorBrush(ColorFromHex("#FF646769")));
                        SetResource(ThemeResourceKey.GroupBoxHeaderBorder.ToString(), new SolidColorBrush(ColorFromHex("#FFA53289")));
                        SetResource(ThemeResourceKey.GroupBoxHeaderForeground.ToString(), new SolidColorBrush(ColorFromHex("#FFA53289")));
                        break;
                    }
                case ThemeType.Dark:
                    {
                        SetResource(ThemeResourceKey.ContentBackground.ToString(), new SolidColorBrush(ColorFromHex("#FF222529")));
                        SetResource(ThemeResourceKey.ContentForeground.ToString(), new SolidColorBrush(ColorFromHex("#FFF4F6F9")));
                        SetResource(ThemeResourceKey.ControlForeground.ToString(), new SolidColorBrush(ColorFromHex("#FFF4F6F9")));
                        SetResource(ThemeResourceKey.ControlBackground.ToString(), new SolidColorBrush(ColorFromHex("#FF2D3136")));
                        SetResource(ThemeResourceKey.ControlBorder.ToString(), new SolidColorBrush(ColorFromHex("#FF616D7B")));
                        SetResource(ThemeResourceKey.ControlContentBackground.ToString(), new SolidColorBrush(ColorFromHex("#FF1C1E22")));
                        SetResource(ThemeResourceKey.ControlHighlightBackground.ToString(), new SolidColorBrush(ColorFromHex("#FF833AB4")));
                        SetResource(ThemeResourceKey.ControlMouseOverBackground.ToString(), new SolidColorBrush(ColorFromHex("#FF4C525E")));
                        SetResource(ThemeResourceKey.ControlPressedBackground.ToString(), new SolidColorBrush(ColorFromHex("#FF222529")));
                        SetResource(ThemeResourceKey.ControlPressedBorder.ToString(), new SolidColorBrush(ColorFromHex("#FF00ACE0")));
                        SetResource(ThemeResourceKey.GlyphForeground.ToString(), new SolidColorBrush(ColorFromHex("#FFEBECED")));
                        SetResource(ThemeResourceKey.GroupBoxHeaderBorder.ToString(), new SolidColorBrush(ColorFromHex("#FFBF399E")));
                        SetResource(ThemeResourceKey.GroupBoxHeaderForeground.ToString(), new SolidColorBrush(ColorFromHex("#FFBF399E")));
                        break;
                    }
            }
        }

        private static LinearGradientBrush GetWindowHeaderGradient()
        {
            return new LinearGradientBrush
            {
                StartPoint = new Point(0, 0.5),

                EndPoint = new Point(1, 0.5),
                GradientStops = new GradientStopCollection
                    {
                        new GradientStop { Offset = 0, Color = ColorFromHex("#0B0D17")},
                        new GradientStop { Offset = 1, Color = ColorFromHex("#0B0D17") }
                    }
            };
        }

        public static object GetResource(ThemeResourceKey resourceKey)
        {
            return ResourceDictionary.Contains(resourceKey.ToString()) ? ResourceDictionary[resourceKey.ToString()] : null;
        }

        internal static void SetResource(object key, object resource)
        {
            ResourceDictionary[key] = resource;
        }

        internal static Color ColorFromHex(string colorHex)
        {
            return (Color?)ColorConverter.ConvertFromString(colorHex) ?? Colors.Transparent;
        }
    }
}