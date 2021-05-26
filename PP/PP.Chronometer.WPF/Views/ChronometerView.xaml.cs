using System.ComponentModel;
using System.Windows;

namespace PP.Chronometer.WPF.Views
{
    /// <summary>
    /// Interaction logic for ChronometerView.xaml
    /// </summary>
    public partial class ChronometerView
    {
        public ChronometerView()
        {
            InitializeComponent();
            DevExpress.Xpf.Core.DXGridDataController.DisableThreadingProblemsDetection = true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void ButtonStop_OnClick(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}