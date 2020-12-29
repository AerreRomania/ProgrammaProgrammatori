using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.EditForm;
using PP.Domain.Columns;
using PP.WPF.ViewModels;
using System;
using System.Collections.Generic;

namespace PP.WPF.Views.EditGridForm
{
    internal class GridTableView : TableView
    {
        protected override IEditFormOwner CreateEditFormOwner()
        {
            var editFormOwner = new EditFormOwnerEx(this);
            editFormOwner.EditFormOpened += EditFormOwner_EditFormOpened;
            editFormOwner.EditFormClosed += EditFormOwner_EditFormClosed;
            return editFormOwner;
        }

        private void EditFormOwner_EditFormClosed(object sender, EventArgs e)
        {
            var source = (EditFormOwnerEx)sender;
            var s = (RowData)source.Source;
            var rowData = (ArticleGridColumns)s.Row;
            ((HomeViewModel)DataContext).SelectedArticleRow = rowData;
            EditFormClosed?.Invoke(this, new EventArgs());
        }

        public event EventHandler EditFormOpened;

        public event EventHandler EditFormClosed;

        private void EditFormOwner_EditFormOpened(object sender, EventArgs e)
        {
            EditFormOpened?.Invoke(this, e);
        }
    }

    public class EditFormOwnerEx : EditFormOwner, IEditFormOwner
    {
        public EditFormOwnerEx(ITableView view) : base(view)
        {
        }

        public event EventHandler EditFormOpened;

        public event EventHandler EditFormClosed;

        public new void OnInlineFormClosed(bool success)
        {
            base.OnInlineFormClosed(success);
            EditFormClosed?.Invoke(this, new EventArgs());
        }

        public new IEnumerable<EditFormColumnSource> CreateEditFormColumnSource()
        {
            EditFormOpened?.Invoke(this, new EventArgs());

            return base.CreateEditFormColumnSource();
        }
    }
}