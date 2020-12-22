using System.Windows;
using System.Windows.Controls;
using TimeScaleCollection = DevExpress.Xpf.Scheduling.TimeScaleCollection;
using TimeScaleHour = DevExpress.Xpf.Scheduling.TimeScaleHour;

namespace PP.WPF.Views
{
    /// <summary>
    /// Interaction logic for TrackingView.xaml
    /// </summary>
    public partial class TrackingView : UserControl
    {
        public TrackingView()
        {
            InitializeComponent();
        }

        private void TrackingView_OnLoaded(object sender, RoutedEventArgs e)
        {
            TimeScaleCollection scales = ProgrammersTimelineControl.Views.TimelineView.TimeScales;
            scales.Clear();

            scales.Add(new TimeScaleHour());

        }
    }
}
