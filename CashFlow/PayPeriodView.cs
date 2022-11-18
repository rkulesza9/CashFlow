using CashFlowData;
using CashFlowGlobals;
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

namespace CashFlow
{
    public partial class PayPeriodView : Form
    {
        private ArrayList m_pData;

        public PayPeriodView()
        {
            InitializeComponent();
            pgTrans.PropertyValueChanged += PgTrans_PropertyValueChanged;
            WindowState = FormWindowState.Maximized;
            lvTrans.ListViewItemSorter = new CListViewSorter(CUIType.PayPeriodView_ListView, 0, SortOrder.Ascending);
            lvTrans.ColumnClick += LvTrans_ColumnClick;

            m_pData = new ArrayList();
        }

        private void LvTrans_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortOrder pSortOrder = SortOrder.Ascending;
            CListViewSorter sorter = (CListViewSorter)lvTrans.ListViewItemSorter;
            if (pSortOrder == sorter.m_pOrder) pSortOrder = SortOrder.Descending;
            lvTrans.ListViewItemSorter = new CListViewSorter(CUIType.PayPeriodView_ListView, e.Column, pSortOrder);
        }

        private void PgTrans_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                foreach (ColumnHeader col in lvTrans.Columns) col.Width = -2;
                UpdateTotals();
            }catch(Exception ex)
            {
                MessageBox.Show("PgTrans_PropertyValueChanged");
                Debug.WriteLine(ex);
            }
        }

        private void PopulateListView(ArrayList data)
        {
            lvTrans.BeginUpdate();
            lvTrans.Items.Clear();
            foreach(CTransaction trans in data)
            {
                ListViewItem item = trans.CreateListViewItem(CUIType.PayPeriodView_ListView);
                lvTrans.Items.Add(item);
            }
            foreach (ColumnHeader col in lvTrans.Columns) col.Width = -2;
            lvTrans.EndUpdate();
        }
        private void UpdateTotals()
        {
            decimal expIncome = 0M;
            decimal expBills = 0M;
            decimal expSpend = 0M;
            decimal absSpend = 0M;
            decimal absBills = 0M;
            decimal expFree = 0M;
            decimal actIncome = 0M;
            decimal actBills = 0M;
            decimal actSpend = 0M;
            decimal actFree = 0M;
            decimal actCredit = 0M;
            decimal expCredit = 0M;

            foreach(CTransaction trans in CData.GetTransactions(true))
            {
                DateTime dt1 = dtFrom.Value.Date;
                DateTime dt2 = dtTo.Value.Date;
                if (dt1 > trans.dtTransaction.Date || trans.dtTransaction.Date >= dt2) continue;

                CAccount acc = CData.GetAccountByID(trans.m_nAccountToID);
                int nDirection = acc.m_pType == CAccountType.External ? -1 : 1;
                

                if(trans.m_pType == CTransactionType.Income)
                {
                    expIncome += nDirection * trans.m_nCost;
                    actIncome += nDirection * trans.m_nAmtPaid;
                }
                else if(trans.m_pType == CTransactionType.Bill)
                {
                    expBills += nDirection * trans.m_nCost;
                    actBills += nDirection * trans.m_nAmtPaid;
                }
                else if(trans.m_pType == CTransactionType.Spend)
                {
                    expSpend += nDirection * trans.m_nCost;
                    actSpend += nDirection * trans.m_nAmtPaid;

                    if(nDirection == -1)
                    {
                        actFree -= trans.m_nCost;
                        expFree -= trans.m_nAmtPaid;
                    }
                }
                else if(trans.m_pType == CTransactionType.Credit)
                {
                    expCredit += nDirection * trans.m_nCost;
                    actCredit += nDirection * trans.m_nAmtPaid;
                }
            }

            expIncome = Math.Abs(expIncome);
            actIncome = Math.Abs(actIncome);
            expBills = Math.Abs(expBills);
            actBills = Math.Abs(actBills);
            expSpend = Math.Abs(expSpend);
            actSpend = Math.Abs(actSpend);
            expCredit = Math.Abs(expCredit);
            actCredit = Math.Abs(actCredit);

            expFree += expIncome - (expBills + expSpend);
            actFree += actIncome - (actBills + actSpend);

            lblExpIncome.Text = expIncome.ToString("c");
            lblActIncome.Text = actIncome.ToString("c");
            lblExpBills.Text = expBills.ToString("c");
            lblActBills.Text = actBills.ToString("c");
            lblExpSpend.Text = expSpend.ToString("c");
            lblActSpend.Text = actSpend.ToString("C");
            lblExpFree.Text = expFree.ToString("c");
            lblActFree.Text = actFree.ToString("C");
            lblExpCredit.Text = expCredit.ToString("C");
            lblActCredit.Text = actCredit.ToString("C");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt1 = dtFrom.Value;
                DateTime dt2 = dtTo.Value;
                m_pData = CData.CreateScheduledTransactions(dt1, dt2);

                PopulateListView(m_pData);
                UpdateTotals();
            }catch(Exception ex)
            {
                MessageBox.Show("btnSubmit_Click");
                Debug.WriteLine(ex);
            }
        }

        private void lvTrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvTrans.SelectedItems.Count == 0) return;

                ListViewItem item = lvTrans.SelectedItems[0];
                CListViewTag tag = (CListViewTag)item.Tag;
                pgTrans.SelectedObject = tag.m_pData;
            }
            catch (Exception ex)
            {
                MessageBox.Show("lvTrans_SelectedIndexChanged");
                Debug.WriteLine(ex);
            }

        }

        
    }
}
