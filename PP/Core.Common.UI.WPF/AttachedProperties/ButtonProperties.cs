using System.Windows;

namespace Core.Common.UI.WPF.AttachedProperties
{
    public static class ButtonProperties
    {
        #region CornerRadius (Attached Property)

        /// <summary>
        /// Identifies the CornerRadius property.
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius",
                typeof(double),
                typeof(ButtonProperties),
                new FrameworkPropertyMetadata(0d));

        /// <summary>
        /// Gets the corner radius.
        /// </summary>
        public static double GetCornerRadius(DependencyObject obj)
        {
            return (double)obj.GetValue(CornerRadiusProperty);
        }

        /// <summary>
        /// Sets the corner radius.
        /// </summary>
        public static void SetCornerRadius(DependencyObject obj, double value)
        {
            obj.SetValue(CornerRadiusProperty, value);
        }

        #endregion CornerRadius (Attached Property)
    }
}