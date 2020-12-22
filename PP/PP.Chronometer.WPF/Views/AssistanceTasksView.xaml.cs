using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;
using PP.Chronometer.WPF.ViewModels;
using PP.Domain.Columns;

namespace PP.Chronometer.WPF.Views
{
    /// <summary>
    /// Interaction logic for AssistanceTasksView.xaml
    /// </summary>
    public partial class AssistanceTasksView : UserControl
    {
        public AssistanceTasksView()
        {
            InitializeComponent();
        }

        private void InProgressTaskTable_OnRowDoubleClick(object sender, RowDoubleClickEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                var row = InProgressGrid.GetRow(e.HitInfo.RowHandle);
                if (row is ProgrammerGridColumns columns)
                {
                    columns.Assistance = true;
                    ((AssistanceTasksViewModel)DataContext).SelectedRow = columns;
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
                    columns.Assistance = true;
                    columns.Repair = true;
                    ((AssistanceTasksViewModel)DataContext).SelectedRow = columns;
                    Application.Current.MainWindow?.Hide();
                }
            }
        }

        
    }
}
