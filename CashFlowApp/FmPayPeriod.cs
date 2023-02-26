using System;
using System.Collections;
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
    public partial class FmPayPeriod : Form
    {
        public CPayPeriod m_pPayPeriod;
        public FmPayPeriod(CPayPeriod pp)
        {
            try
            {
                InitializeComponent();
                lvBills.ListViewItemSorter = new CListViewComparer(CDefines.UI_LISTVIEW_PAYPERIOD, 0, SortOrder.Ascending);
                lvCredit.ListViewItemSorter = new CListViewComparer(CDefines.UI_LISTVIEW_PAYPERIOD, 0, SortOrder.Ascending);
                lvBills.ColumnClick += LvBills_ColumnClick;
                lvCredit.ColumnClick += LvCredit_ColumnClick;
                pgEdit.PropertyValueChanged += PgEdit_PropertyValueChanged;
                m_pPayPeriod = pp;
                

                if (!Properties.Settings.Default[CDefines.SETTINGS_PAYPERIOD_DATE_FROM].Equals(new DateTime()))
                {
                    pp.m_dtStart = (DateTime)Properties.Settings.Default[CDefines.SETTINGS_PAYPERIOD_DATE_FROM];
                }
                if (!Properties.Settings.Default[CDefines.SETTINGS_PAYPERIOD_DATE_TO].Equals(new DateTime()))
                {
                    pp.m_dtEnd = (DateTime)Properties.Settings.Default[CDefines.SETTINGS_PAYPERIOD_DATE_TO];
                }
                

                PopulateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FmPayPeriod");
                Debug.WriteLine(ex);
            }
        }


        #region "events"
        private void PgEdit_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                foreach (CColHdr col in lvBills.Columns)
                {
                    col.Width = -2;
                }
                foreach (CColHdr col in lvCredit.Columns)
                {
                    col.Width = -2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("pgEdit_PropertyValueChanged");
                Debug.WriteLine(ex);
            }
        }
        private void LvCredit_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                CListViewComparer comparer = (CListViewComparer)lvCredit.ListViewItemSorter;
                SortOrder pOrder = SortOrder.Ascending;
                if (pOrder == comparer.m_pSortOrder) pOrder = SortOrder.Descending;
                lvCredit.ListViewItemSorter = new CListViewComparer(CDefines.UI_LISTVIEW_PAYPERIOD, e.Column, pOrder);

                m_pPayPeriod.UpdateTotals();
                PopulateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("LvCredit_ColumnClick");
                Debug.WriteLine(ex);
            }
        }

        private void LvBills_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                CListViewComparer comparer = (CListViewComparer)lvBills.ListViewItemSorter;
                SortOrder pOrder = SortOrder.Ascending;
                if (pOrder == comparer.m_pSortOrder) pOrder = SortOrder.Descending;
                lvBills.ListViewItemSorter = new CListViewComparer(CDefines.UI_LISTVIEW_PAYPERIOD, e.Column, pOrder);

                m_pPayPeriod.UpdateTotals();
                PopulateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("LvBills_ColumnClick");
                Debug.WriteLine(ex);
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default[CDefines.SETTINGS_PAYPERIOD_DATE_FROM] = dtFrom.Value;
                Properties.Settings.Default[CDefines.SETTINGS_PAYPERIOD_DATE_TO] = dtTo.Value;
                Properties.Settings.Default.Save();
                m_pPayPeriod = new CPayPeriod(dtFrom.Value, dtTo.Value, CJsonDatabase.Instance.GetActiveTransactions(""));
                PopulateUI();
            }
            catch(Exception ex)
            {
                MessageBox.Show("btnSubmit_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                CTransaction trans = (CTransaction) CJsonDatabase.Instance.Fetch(CDefines.TYPE_TRANSACTION, "");
                trans.dtStartDate = DateTime.Now;
                trans.m_nTransTypeID = CDefines.TRANS_TYPE_CREDIT;

                m_pPayPeriod.m_lsCredit.Add(trans);

                m_pPayPeriod.UpdateTotals();
                PopulateUI();
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
                if (lvCredit.SelectedItems.Count == 0) return;
                CListViewItem item = (CListViewItem)lvCredit.SelectedItems[0];
                CTransaction trans = (CTransaction)item.Tag;
                trans.bArchived = true;

                lvCredit.Items.Remove(item);
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
                if (lvCredit.SelectedItems.Count == 0) return;
                CListViewItem item = (CListViewItem)lvCredit.SelectedItems[0];
                CTransaction trans = (CTransaction)item.Tag;
                trans.bDeleted = true;

                lvCredit.Items.Remove(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnRemove_Click");
                Debug.WriteLine(ex);
            }

        }
        private void lvBills_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvBills.SelectedItems.Count == 0) return;
                CListViewItem item = (CListViewItem)lvBills.SelectedItems[0];
                CTransaction trans = (CTransaction)item.Tag;

                pgEdit.SelectedObject = trans;
            }
            catch (Exception ex)
            {
                MessageBox.Show("lvBills_SelectedIndexChanged");
                Debug.WriteLine(ex);
            }
        }

        private void lvCredit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvCredit.SelectedItems.Count == 0) return;
                CListViewItem item = (CListViewItem)lvCredit.SelectedItems[0];
                CTransaction trans = (CTransaction)item.Tag;

                pgEdit.SelectedObject = trans;

            }
            catch (Exception ex)
            {
                MessageBox.Show("lvCredit_SelectedIndexChanged");
                Debug.WriteLine(ex);
            }
        }
        #endregion

        public void PopulateUI()
        {
            ArrayList lsBillsIncome = new ArrayList();
            lsBillsIncome.AddRange(m_pPayPeriod.m_lsBills.ToArray());
            lsBillsIncome.AddRange(m_pPayPeriod.m_lsIncome.ToArray());
            PopulateListView(lvBills, lsBillsIncome);
            PopulateListView(lvCredit, m_pPayPeriod.m_lsCredit);

            dtFrom.Value = m_pPayPeriod.m_dtStart;
            dtTo.Value = m_pPayPeriod.m_dtEnd;
            lblActBills.Text = m_pPayPeriod.m_nActBillsTotal.ToString("C");
            lblActIncome.Text = m_pPayPeriod.m_nActIncomeTotal.ToString("C");
            lblActFree.Text = (m_pPayPeriod.m_nActIncomeTotal - m_pPayPeriod.m_nActBillsTotal).ToString("C");
            lblExpBills.Text = m_pPayPeriod.m_nExpBillsTotal.ToString("C");
            lblExpIncome.Text = m_pPayPeriod.m_nExpIncomeTotal.ToString("C");
            lblExpFree.Text = (m_pPayPeriod.m_nExpIncomeTotal - m_pPayPeriod.m_nExpBillsTotal).ToString("C");
            lblCreditTotal.Text = m_pPayPeriod.m_nCreditTotal.ToString("C");
        }

        public void PopulateListView(ListView listView, ArrayList data) 
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            listView.Columns.Clear();
            listView.Columns.AddRange(CDefines.UI_LISTVIEW_PAYPERIOD_COLUMNS);
            foreach (CTransaction trans in data)
            {
                CListViewItem item = trans.CreateListViewItem(CDefines.UI_LISTVIEW_PAYPERIOD);
                listView.Items.Add(item);
            }

            foreach (CColHdr col in listView.Columns)
            {
                col.Width = -2;
            }
            listView.EndUpdate();
        }
    }
}
