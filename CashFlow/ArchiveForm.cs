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
    public partial class ArchiveForm : Form
    {
        private ArrayList m_pData;
        private CHeader[] m_pColumns;
        public string m_szFilename;
        public CUIType m_pType;

        public ArchiveForm()
        {
            InitializeComponent();
            lvArchive.ColumnClick += LvArchive_ColumnClick;
        }

        private void LvArchive_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                CListViewSorter sorter = (CListViewSorter)lvArchive.ListViewItemSorter;
                SortOrder pOrder = SortOrder.Ascending;
                if (e.Column == sorter.m_nColumn && pOrder == sorter.m_pOrder) pOrder = SortOrder.Descending;
                lvArchive.ListViewItemSorter = new CListViewSorter(m_pType, e.Column, pOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show("LvArchive_ColumnClick");
                Debug.WriteLine(ex);
            }
        }

        private void btnUnarchive_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvArchive.SelectedItems.Count == 0) return;

                ListViewItem item = lvArchive.SelectedItems[0];
                CListViewTag tag = (CListViewTag)item.Tag;
                CBaseData data = tag.m_pData;

                data.bArchived = false;
                m_pData.Remove(data);
                PopulateListView(m_pData);
                
            }catch(Exception ex)
            {
                MessageBox.Show("btnUnarchive_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvArchive.SelectedItems.Count == 0) return;

                ListViewItem item = lvArchive.SelectedItems[0];
                CListViewTag tag = (CListViewTag)item.Tag;
                CBaseData data = tag.m_pData;

                if (DialogResult.Yes != MessageBox.Show("Are You Sure You want To Delete This Record?", "Confirm Delete", MessageBoxButtons.YesNo)) return;

                data.bDeleted = true;
                m_pData.Remove(data);
                PopulateListView(m_pData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnDelete_Click");
                Debug.WriteLine(ex);
            }
        }
        public void Initialize(Form parent)
        {
            try
            {

                switch (m_pType)
                {
                    case CUIType.ListView_Accounts:
                        m_pData = CData.GetArchivedAccounts();
                        m_pColumns = CListViewColumns.GetColumnsFor(m_pType);
                        Text = "View Accounts";
                        break;
                    case CUIType.ListView_Transactions:
                        m_pData = CData.GetArchivedTransactions();
                        m_pColumns = CListViewColumns.GetColumnsFor(m_pType);
                        Text = "View Transactions";
                        break;
                    case CUIType.ListView_Schedules:
                        m_pData = CData.GetArchivedSchedules();
                        m_pColumns = CListViewColumns.GetColumnsFor(m_pType);
                        Text = "View Schedules";
                        break;
                    default:
                        break;
                }

                AddHeaders(m_pColumns);
                PopulateListView(m_pData);

                lvArchive.ListViewItemSorter = new CListViewSorter(m_pType, 0, SortOrder.Ascending);

                MdiParent = parent;
                Show();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("Initialize");
            }
        }
        public void AddHeaders(CHeader[] headers)
        {
            try
            {
                lvArchive.BeginUpdate();
                lvArchive.Columns.Clear();

                foreach (CHeader header in headers)
                {
                    lvArchive.Columns.Add(header);
                }
                lvArchive.EndUpdate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("AddHeaders");
            }
        }
        public void PopulateListView(ArrayList data)
        {
            try
            {
                lvArchive.BeginUpdate();
                lvArchive.Items.Clear();
                foreach (CBaseData item in data)
                {
                    lvArchive.Items.Add(item.CreateListViewItem(m_pType));
                }

                foreach (CHeader header in m_pColumns)
                {
                    header.Width = -2;
                }
                lvArchive.EndUpdate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("PopulateListView(data)");
            }
        }
    }
}
