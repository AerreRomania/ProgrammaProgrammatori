using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PP.Chronometer.WPF.Controls
{
    /// <summary>
    /// Interaction logic for LoadingControl.xaml
    /// </summary>
    public partial class LoadingControl : UserControl
    {
        public LoadingControl()
        {
            InitializeComponent();
        }

        #region Diameter

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the diameter.</summary>
        ///
        /// <value>The diameter.</value>
        ///-------------------------------------------------------------------------------------------------

        public int Diameter
        {
            get { return (int)GetValue(DiameterProperty); }
            set
            {
                if (value < 10)
                    value = 10;
                SetValue(DiameterProperty, value);
            }
        }

        public static readonly DependencyProperty DiameterProperty =    ///< The diameter property
            DependencyProperty.Register("Diameter", typeof(int), typeof(LoadingControl), new PropertyMetadata(20, OnDiameterPropertyChanged));

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Raises the dependency property changed event.</summary>
        ///
        /// <remarks>Software Developer Aleksandar Vučković - Aerre Romania, 07/05/2020.</remarks>
        ///
        /// <param name="d">A DependencyObject to process.</param>
        /// <param name="e">Event information to send to registered event handlers.</param>
        ///-------------------------------------------------------------------------------------------------

        private static void OnDiameterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var vm = (LoadingControl)d;
            d.CoerceValue(CenterProperty);
            d.CoerceValue(RadiusProperty);
            d.CoerceValue(InnerRadiusProperty);
        }

        #endregion Diameter

        #region Radius

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the radius.</summary>
        ///
        /// <value>The radius.</value>
        ///-------------------------------------------------------------------------------------------------

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty RadiusProperty =  ///< The radius property
            DependencyProperty.Register("Radius", typeof(int), typeof(LoadingControl), new PropertyMetadata(15, null, OnCoerceRadius));

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Executes the coerce radius action.</summary>
        ///
        /// <remarks>Software Developer Aleksandar Vučković - Aerre Romania, 07/05/2020.</remarks>
        ///
        /// <param name="d">        A DependencyObject to process.</param>
        /// <param name="baseValue">The base value.</param>
        ///
        /// <returns>An object.</returns>
        ///-------------------------------------------------------------------------------------------------

        private static object OnCoerceRadius(DependencyObject d, object baseValue)
        {
            var control = (LoadingControl)d;
            int newRadius = (int)(control.GetValue(DiameterProperty)) / 2;
            return newRadius;
        }

        #endregion Radius

        #region InnerRadius

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the inner radius.</summary>
        ///
        /// <value>The inner radius.</value>
        ///-------------------------------------------------------------------------------------------------

        public int InnerRadius
        {
            get { return (int)GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }

        public static readonly DependencyProperty InnerRadiusProperty = ///< The inner radius property
            DependencyProperty.Register("InnerRadius", typeof(int), typeof(LoadingControl), new PropertyMetadata(2, null, OnCoerceInnerRadius));

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Executes the coerce inner radius action.</summary>
        ///
        /// <remarks>Software Developer Aleksandar Vučković - Aerre Romania, 07/05/2020.</remarks>
        ///
        /// <param name="d">        A DependencyObject to process.</param>
        /// <param name="baseValue">The base value.</param>
        ///
        /// <returns>An object.</returns>
        ///-------------------------------------------------------------------------------------------------

        private static object OnCoerceInnerRadius(DependencyObject d, object baseValue)
        {
            var control = (LoadingControl)d;
            int newInnerRadius = (int)(control.GetValue(DiameterProperty)) / 4;
            return newInnerRadius;
        }

        #endregion InnerRadius

        #region Center

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the center.</summary>
        ///
        /// <value>The center.</value>
        ///-------------------------------------------------------------------------------------------------

        public Point Center
        {
            get { return (Point)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        public static readonly DependencyProperty CenterProperty =  ///< The center property
            DependencyProperty.Register("Center", typeof(Point), typeof(LoadingControl), new PropertyMetadata(new Point(15, 15), null, OnCoerceCenter));

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Executes the coerce center action.</summary>
        ///
        /// <remarks>Software Developer Aleksandar Vučković - Aerre Romania, 07/05/2020.</remarks>
        ///
        /// <param name="d">        A DependencyObject to process.</param>
        /// <param name="baseValue">The base value.</param>
        ///
        /// <returns>An object.</returns>
        ///-------------------------------------------------------------------------------------------------

        private static object OnCoerceCenter(DependencyObject d, object baseValue)
        {
            var control = (LoadingControl)d;
            int newCenter = (int)(control.GetValue(DiameterProperty)) / 2;
            return new Point(newCenter, newCenter);
        }

        #endregion Center

        #region Color1

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the color 1.</summary>
        ///
        /// <value>The color 1.</value>
        ///-------------------------------------------------------------------------------------------------

        public Color Color1
        {
            get { return (Color)GetValue(Color1Property); }
            set { SetValue(Color1Property, value); }
        }

        public static readonly DependencyProperty Color1Property =  ///< The color 1 property
            DependencyProperty.Register("Color1", typeof(Color), typeof(LoadingControl), new PropertyMetadata(Colors.Green));

        #endregion Color1

        #region Color2

        ///-------------------------------------------------------------------------------------------------
        /// <summary>Gets or sets the color 2.</summary>
        ///
        /// <value>The color 2.</value>
        ///-------------------------------------------------------------------------------------------------

        public Color Color2
        {
            get { return (Color)GetValue(Color2Property); }
            set { SetValue(Color2Property, value); }
        }

        public static readonly DependencyProperty Color2Property =  ///< The color 2 property
            DependencyProperty.Register("Color2", typeof(Color), typeof(LoadingControl), new PropertyMetadata(Colors.Transparent));

        #endregion Color2
    }
}