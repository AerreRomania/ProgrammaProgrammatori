using System;
using System.Windows.Threading;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;

namespace PP.Chronometer.WPF.Behaviors
{
    public class BestFitBehavior : Behavior<GridControl>
    {
        protected override void OnAttached()
        {
            AssociatedObject.ItemsSourceChanged += Grid_ItemsSourceChanged;
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            base.OnAttached();
        }
        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.ItemsSourceChanged -= Grid_ItemsSourceChanged;
            base.OnDetaching();
        }
        //Initial Best Fit
        private void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ((TableView)AssociatedObject.View).BestFitColumns();
        }

        //When a new source is assigned
        private void Grid_ItemsSourceChanged(object sender, ItemsSourceChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => ((TableView)AssociatedObject.View).BestFitColumns()), DispatcherPriority.Render);
        }
    }
}
