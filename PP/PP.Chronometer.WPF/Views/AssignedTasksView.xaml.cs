using DevExpress.Xpf.Grid;
using PP.Chronometer.WPF.ViewModels;
using PP.Domain.Columns;
using System.Windows;
using System.Windows.Controls;

namespace PP.Chronometer.WPF.Views
{
    /// <summary>
    /// Interaction logic for AssignedTasksView.xaml
    /// </summary>
    public partial class AssignedTasksView : UserControl
    {
        public AssignedTasksView()
        {
            InitializeComponent();
            DevExpress.Xpf.Core.DXGridDataController.DisableThreadingProblemsDetection = true;
        }

        private void InProgressTaskTable_OnRowDoubleClick(object sender, RowDoubleClickEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                var row = InProgressGrid.GetRow(e.HitInfo.RowHandle);
                if (row is ProgrammerGridColumns columns)
                {
                    ((AssignedTasksViewModel)DataContext).SelectedRow = columns;
                    Application.Current.MainWindow?.Hide();
                }
            }
        }

        private void TableFinishedArticles_OnRowDoubleClick(object sender, RowDoubleClickEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                var row = InProgressGrid.GetRow(e.HitInfo.RowHandle);
                if (row is ProgrammerGridColumns columns)
                {
                    columns.Repair = true;
                    ((AssignedTasksViewModel)DataContext).SelectedRow = columns;
                    Application.Current.MainWindow?.Hide();
                }
            }
        }
    }
}