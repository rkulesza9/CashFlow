using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowApp
{
    public partial class FmTransaction : Form
    {
        public FmTransaction()
        {
            InitializeComponent();
            pgEditor.PropertyValueChanged += PgEditor_PropertyValueChanged;
            lvDataView.ColumnClick += LvDataView_ColumnClick;
            lvDataView.ListViewItemSorter = new CListViewComparer(CDefines.UI_LISTVIEW_TRANS, 0, SortOrder.Ascending);

            PopulateTransactions("");
        }

        #region "Events"
        private void LvDataView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                CListViewComparer comparer = (CListViewComparer)lvDataView.ListViewItemSorter;
                SortOrder pOrder = SortOrder.Ascending;
                if (pOrder == comparer.m_pSortOrder) pOrder = SortOrder.Descending;
                lvDataView.ListViewItemSorter = new CListViewComparer(CDefines.UI_LISTVIEW_TRANS, e.Column, pOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("LvDataView_ColumnClick");
                Debug.WriteLine(ex);
            }
        }
        private void PgEditor_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                foreach(CColHdr col in lvDataView.Columns)
                {
                    col.Width = -2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PgEditor_PropertyValueChanged");
                Debug.WriteLine(ex);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateTransactions(tbSearch.Text);
                pgEditor.SelectedObject = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnSearch_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                PopulateTransactions("");
                tbSearch.Text = "";
                pgEditor.SelectedObject = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnRefresh_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CTransaction trans = (CTransaction) CJsonDatabase.Instance.Fetch(CDefines.TYPE_TRANSACTION, "");
                CListViewItem item = trans.CreateListViewItem(CDefines.UI_LISTVIEW_TRANS);
                lvDataView.Items.Add(item);
                item.Selected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnAdd_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvDataView.SelectedItems.Count == 0) return;
                CListViewItem item = (CListViewItem)lvDataView.SelectedItems[0];
                CTransaction trans = (CTransaction)item.Tag;

                trans.bArchived = true;
                lvDataView.Items.Remove(item);
                pgEditor.SelectedObject = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnArchive_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvDataView.SelectedItems.Count == 0) return;
                CListViewItem item = (CListViewItem)lvDataView.SelectedItems[0];
                CTransaction trans = (CTransaction)item.Tag;

                trans.bDeleted = true;
                lvDataView.Items.Remove(item);
                pgEditor.SelectedObject = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnRemove_Click");
                Debug.WriteLine(ex);
            }
        }
        private void lvDataView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvDataView.SelectedItems.Count == 0) return;
                CListViewItem item = (CListViewItem)lvDataView.SelectedItems[0];
                CTransaction trans = (CTransaction)item.Tag;
                pgEditor.SelectedObject = trans;
            }
            catch (Exception ex)
            {
                MessageBox.Show("lvDataView_SelectedIndexChanged");
                Debug.WriteLine(ex);
            }
        }
        #endregion

        public void PopulateTransactions(string szTerms)
        {
            lvDataView.BeginUpdate();
            lvDataView.Items.Clear();
            lvDataView.Columns.Clear();
            lvDataView.Columns.AddRange(CDefines.UI_LISTVIEW_TRANS_COLUMNS);
            foreach(CTransaction trans in CJsonDatabase.Instance.GetTransactions(szTerms))
            {
                CListViewItem item = trans.CreateListViewItem(CDefines.UI_LISTVIEW_TRANS);
                lvDataView.Items.Add(item);
            }

            foreach(CColHdr col in lvDataView.Columns)
            {
                col.Width = -2;
            }
            lvDataView.EndUpdate();
        }
    }
}
