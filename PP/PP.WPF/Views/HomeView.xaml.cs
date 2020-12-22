using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Scheduling;
using PP.Domain.Columns;
using PP.Domain.Models;
using PP.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PP.WPF.Views
{

    public partial class HomeView
    {
        public HomeView()
        {

            InitializeComponent();
        }

        private void ProgrammersTimelineControl_OnResizeAppointmentOver(object sender, ResizeAppointmentOverEventArgs e)
        {
            if (e.ResizeAppointment.Duration.TotalHours < 1)
            {
                if (e.ResizeHandle == ResizeHandle.End)
                {
                    e.ResizeAppointment.End += TimeSpan.FromHours(1) - e.ResizeAppointment.Duration;
                }
                else
                {
                    DateTime end = e.ResizeAppointment.End;
                    e.ResizeAppointment.Start -= TimeSpan.FromHours(1) - e.ResizeAppointment.Duration;
                    e.ResizeAppointment.End = end;
                }
            }
        }

        private void programmersTimelineControl_StartAppointmentDragFromOutside(object sender, StartAppointmentDragFromOutsideEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(IEnumerable<ArticleGridColumns>)) || 
                !(e.Data.GetData(typeof(IEnumerable<ArticleGridColumns>)) is IEnumerable<ArticleGridColumns> articles)) return;

            var article = articles.FirstOrDefault() ?? throw new ArgumentNullException(nameof(sender));
            if (article.Num == null) return;

            var item = new AppointmentItem
            {
                Subject = article.Articolo,
                Start = programmersTimelineControl.SelectedInterval.Start,
                End = programmersTimelineControl.SelectedInterval.End,
                ResourceId = (int) programmersTimelineControl.SelectedResource.Id,
                LabelId = 0,
                Description = article.Num.ToString()
            };

            e.DragAppointments.Add(item);
        }

        private void ProgrammersTimelineControl_OnDropAppointment(object sender, DropAppointmentEventArgs e)
        {
            for (int i = 0; i < e.DragAppointments.Count; i++)
            {
                if (e.ConflictedAppointments[i].Count == 0)
                {
                    var viewModel = (HomeViewModel)DataContext;
                    viewModel.ProgrammerTask = new ProgrammerTask
                    {
                        ArticleTitle = e.DragAppointments[i].Subject, 
                        StartTask = e.DragAppointments[i].Start,
                        EndTask = e.DragAppointments[i].End,
                        JobTypeID = 0,
                        ProgrammerID = (int)e.DragAppointments[i].ResourceId,
                        ArticleID = Convert.ToInt32(e.DragAppointments[i].Description),
                        TaskCompleted = false
                    };
                }
            }

        }

        private void TableArticles_OnDragRecordOver(object? sender, DragRecordOverEventArgs e)
        {
            if (!e.IsFromOutside || !e.Data.GetDataPresent(typeof(IEnumerable<AppointmentItemData>))) return;
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }


        private void TableArticles_OnStartRecordDrag(object? sender, StartRecordDragEventArgs e)
        {
            e.Data.SetData(typeof(IEnumerable<ArticleGridColumns>), e.Records.Cast<ArticleGridColumns>());
            e.Handled = true;
        }

        private void ProgrammersTimelineControl_OnAppointmentEdited(object sender, AppointmentEditedEventArgs e)
        {
            foreach (var task in e.Appointments)
            {
                var viewModel = (HomeViewModel)DataContext;

                var programmerTask = viewModel.ProgrammerTasks.FirstOrDefault(p =>
                    p.ArticleTitle == task.Subject &&
                    p.ProgrammerID == (int)task.ResourceId &&
                    p.StartTask == task.Start &&
                    p.EndTask == task.End);

                Articole first = viewModel.Articles.FirstOrDefault(id => id.Articol == task.Subject)!;

                        viewModel.ProgrammerTask = new ProgrammerTask
                        {
                            ArticleTitle = task.Subject,
                            StartTask = task.Start,
                            EndTask = task.End,
                            JobTypeID = (int) task.LabelId,
                            ProgrammerID = (int) task.ResourceId,
                            ArticleID = (int) task.CustomFields["ArticleID"] == 0 ? first.Id : (int) task.CustomFields["ArticleID"],
                            ProgrammerTaskID = (int) task.Id == 0 ? programmerTask.ProgrammerTaskID : (int)task.Id,
                            TaskCompleted = Convert.ToBoolean(task.StatusId)
                        };
            }
        }

        private void ProgrammersTimelineControl_OnAppointmentRemoved(object sender, AppointmentRemovedEventArgs e)
        {
            foreach (var task in e.Appointments)
            {
                var viewModel = (HomeViewModel)DataContext;
                viewModel.ProgrammerTask = new ProgrammerTask
                {
                    JobTypeID = (int)task.LabelId,
                    ArticleID = (int)task.CustomFields["ArticleID"] == 0 ? Convert.ToInt32(task.Description) : (int)task.CustomFields["ArticleID"],
                    ProgrammerTaskID = (int)task.Id == 0 ? ((HomeViewModel)DataContext).ProgrammerTask.ProgrammerTaskID : (int)task.Id,
                };
            }
        }

        private void TableArticles_OnCompleteRecordDragDrop(object? sender, CompleteRecordDragDropEventArgs e)
        {
            e.Handled = true;
        }

        private void TableArticles_OnShowingEditor(object sender, ShowingEditorEventArgs e)
        {
            if (e.RowHandle != DataControlBase.NewItemRowHandle)
                e.Cancel = true;
        }

        private void ButtonTutti_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente")
                    continue;

                switch (column.Name)
                {
                    case "ColumnModel":
                    case "ColumnFin":
                    case "ColumnProgrammerPR":
                    case "ColumnDataArrivoSchedePr":
                    case "ColumnStartPr":
                    case "ColumnEndPr":
                    case "ColumnProgrammerCa":
                    case "ColumnStartCa":
                    case "ColumnEndCa":
                    case "ColumnConfCamp":
                    case "ColumnDataArrivoFilo":
                    case "ColumnCapiPrevisti":
                    case "ColumnNrMach":
                    case "ColumnDataEntrataInProd":
                    case "ColumnDataArrivoSchema":
                    case "ColumnDataInizioSvilTgBase":
                    case "ColumnDataFineSvilTgBase":
                    case "ColumnGg1":
                    case "ColumnDataArrivoSchede":
                    case "ColumnDataArrivoDisco":
                    case "ColumnProgrammerPP":
                    case "ColumnStartPP":
                    case "ColumnEndPP":
                    case "ColumnGg2":
                    case "ColumnOk":
                    case "ColumnGg3":
                    case "ColumnProgrammerSv":
                    case "ColumnStartSv":
                    case "ColumnEndSv":
                    case "ColumnDataInizioProd":
                    case "ColumnGg4":
                    case "ColumnFinish":
                    case "ColumnWeights":
                    case "ColumnTime":
                    case "ColumnConfPp":
                    case "ColumnNote":
                        column.Visible = true;
                        break;
                }
            }

            GridArticles.RefreshData();
        }

        private void ButtonPrincipale_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;

                column.Visible = false;
            }

            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;
                switch (column.Name)
                {

                    case "ColumnFin":
                    case "ColumnProgrammerPR":
                    case "ColumnDataArrivoSchedePr":
                    case "ColumnStartPr":
                    case "ColumnEndPr":
                    case "ColumnProgrammerCa":
                    case "ColumnStartCa":
                    case "ColumnEndCa":
                    case "ColumnConfCamp":
                    case "ColumnDataArrivoFilo":
                    case "ColumnDataEntrataInProd":
                    case "ColumnDataArrivoSchema":
                    case "ColumnDataInizioSvilTgBase":
                    case "ColumnDataFineSvilTgBase":
                    case "ColumnGg1":
                    case "ColumnDataArrivoSchede":
                    case "ColumnDataArrivoDisco":
                    case "ColumnProgrammerPP":
                    case "ColumnStartPP":
                    case "ColumnEndPP":
                    case "ColumnOk":
                    case "ColumnGg3":
                    case "ColumnProgrammerSv":
                    case "ColumnStartSv":
                    case "ColumnEndSv":
                    case "ColumnDataInizioProd":
                    case "ColumnGg4":
                    case "ColumnFinish":
                    case "ColumnNote":
                        column.Visible = true;
                        break;
                }

            }

            GridArticles.RefreshData();
        }

        private void ButtonPrCa_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (GridColumn column in TableArticles.Grid.Columns) { if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue; column.Visible = false; }

            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;
                switch (column.Name)
                {
                    case "ColumnModel":
                    case "ColumnFin":
                    case "ColumnProgrammerPR":
                    case "ColumnDataArrivoSchedePr":
                    case "ColumnStartPr":
                    case "ColumnEndPr":
                    case "ColumnProgrammerCa":
                    case "ColumnStartCa":
                    case "ColumnEndCa":
                    case "ColumnConfCamp":
                    case "ColumnDataArrivoFilo":
                    case "ColumnNote":
                        column.Visible = true;
                        break;
                }

            }

            GridArticles.RefreshData();
        }

        private void ButtonScTec_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;

                column.Visible = false;
            }

            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;
                switch (column.Name)
                {

                    case "ColumnModel":
                    case "ColumnFin":
                    case "ColumnCapiPrevisti":
                    case "ColumnNrMach":
                    case "ColumnDataEntrataInProd":
                    case "ColumnDataArrivoSchema":
                    case "ColumnDataInizioSvilTgBase":
                    case "ColumnDataFineSvilTgBase":
                    case "ColumnNote":
                        column.Visible = true;
                        break;
                }

            }

            GridArticles.RefreshData();
        }

        private void ButtonPP_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;

                column.Visible = false;
            }

            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;
                switch (column.Name)
                {

                    case "ColumnModel":
                    case "ColumnFin":
                    case "ColumnDataEntrataInProd":
                    case "ColumnDataArrivoSchede":
                    case "ColumnDataArrivoDisco":
                    case "ColumnProgrammerPP":
                    case "ColumnStartPP":
                    case "ColumnEndPP":
                    case "ColumnGg2":
                    case "ColumnOk":
                    case "ColumnGg3":
                    case "ColumnNote":
                        column.Visible = true;
                        break;
                }

            }

            GridArticles.RefreshData();
        }

        private void ButtonSviluppo_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;

                column.Visible = false;
            }

            foreach (GridColumn column in TableArticles.Grid.Columns)
            {
                if (column.FieldName == "Num" || column.FieldName == "Articolo" || column.FieldName == "Cliente") continue;
                switch (column.Name)
                {
                    case "ColumnOk":
                    case "ColumnProgrammerSv":
                    case "ColumnStartSv":
                    case "ColumnEndSv":
                    case "ColumnDataInizioProd":
                    case "ColumnGg4":
                    case "ColumnFinish":
                    case "ColumnWeights":
                    case "ColumnTime":
                    case "ColumnConfPp":
                    case "ColumnNote":
                        column.Visible = true;
                        break;
                }

            }

            GridArticles.RefreshData();
        }


    }

}
