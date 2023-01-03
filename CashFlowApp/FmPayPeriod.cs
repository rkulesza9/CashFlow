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
                m_pPayPeriod = pp;

                PopulateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FmPayPeriod");
                Debug.WriteLine(ex);
            }
        }

        #region "events"
        private void LvCredit_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                CListViewComparer comparer = (CListViewComparer)lvCredit.ListViewItemSorter;
                SortOrder pOrder = SortOrder.Ascending;
                if (pOrder == comparer.m_pSortOrder) pOrder = SortOrder.Descending;
                lvCredit.ListViewItemSorter = new CListViewComparer(CDefines.UI_LISTVIEW_PAYPERIOD, e.Column, pOrder);
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

            }catch(Exception ex)
            {
                MessageBox.Show("btnSubmit_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

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
