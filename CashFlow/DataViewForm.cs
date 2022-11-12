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
    public partial class DataViewForm : Form
    {
        #region "Members & Constructors"
        private ArrayList m_pData;
        private CHeader[] m_pColumns;
        public string m_szFilename;
        public CUIType m_pType;

        public DataViewForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            pgEditor.PropertyValueChanged += PgEditor_PropertyValueChanged;
        }

        private void PgEditor_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            try
            {
                pgEditor.Refresh();
                foreach (CHeader h in m_pColumns) h.Width = -2;
            }catch(Exception ex)
            {
                MessageBox.Show("PgEditor_PropertyValueChanged");
                Debug.WriteLine(ex);
            }
        }
        #endregion

        public void Initialize(Form parent)
        {
            try
            {
                switch (m_pType)
                {
                    case CUIType.DataViewForm_Accounts:
                        m_pData = CData.GetAccounts();
                        m_pColumns = CConstants.COLUMNS_ACCOUNT;
                        Text = "View Accounts";
                        break;
                    case CUIType.DataViewForm_Transactions:
                        m_pData = CData.GetTransactions();
                        m_pColumns = CConstants.COLUMNS_TRANSACTION;
                        Text = "View Transactions";
                        break;
                    case CUIType.DataViewForm_Schedules:
                        m_pData= CData.GetSchedules();
                        m_pColumns = CConstants.COLUMNS_SCHEDULE;
                        Text = "View Schedules";
                        break;
                    default:
                        break;
                }

                AddHeaders(m_pColumns);
                PopulateListView(m_pData);
                //lblFileName.Text = m_szFilename;

                MdiParent = parent;
                Show();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("Initialize");
            }
        } 
        public void AddHeaders(CHeader[] headers)
        {
            try
            {
                lvDataView.BeginUpdate();
                lvDataView.Columns.Clear();

                foreach(CHeader header in headers)
                {
                    lvDataView.Columns.Add(header); 
                }
                lvDataView.EndUpdate();
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
                lvDataView.BeginUpdate();
                lvDataView.Items.Clear();
                foreach(CBaseData item in data)
                {
                    lvDataView.Items.Add(item.CreateListViewItem(m_pType));
                }

                foreach(CHeader header in m_pColumns)
                {
                    header.Width = -2;
                }
                lvDataView.EndUpdate();
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("PopulateListView(data)");
            }
        }

        #region "Event Listeners"
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lvDataView.SelectedItems.Count == 0) return;
                CListViewTag tag = (CListViewTag) lvDataView.SelectedItems[0].Tag;
                pgEditor.SelectedObject = tag.m_pData;
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("listView1_SelectedIndexChanged");
            }
        }
        private void btnArchive_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvDataView.SelectedItems.Count == 0) return;
                if (MessageBox.Show("Are You Sure You Want To Archive This Item?", "Confirm Archive", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ListViewItem item = lvDataView.SelectedItems[0];
                    CListViewTag tag = (CListViewTag)item.Tag;
                    CBaseData acc = (CBaseData)tag.m_pData;
                    acc.m_bArchived = true;
                    m_pData.Remove(acc);
                    lvDataView.Items.Remove(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("btnArchive_Click");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvDataView.SelectedItems.Count == 0) return;
                if (MessageBox.Show("Are You Sure You Want To Delete This Item?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ListViewItem item = lvDataView.SelectedItems[0];
                    CListViewTag tag = (CListViewTag)item.Tag;
                    CBaseData acc = (CBaseData)tag.m_pData;
                    acc.m_bDeleted = true;
                    m_pData.Remove(acc);
                    lvDataView.Items.Remove(item);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("btnRemove_Click");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem item = new ListViewItem();
                CBaseData newData = new CBaseData();
                switch (m_pType)
                {
                    case CUIType.DataViewForm_Accounts:
                        newData = CData.NewAccount();
                        break;
                    case CUIType.DataViewForm_Transactions:
                        newData = CData.NewTransaction();
                        break;
                    case CUIType.DataViewForm_Schedules:
                        newData = CData.NewSchedule();
                        break;
                    default:
                        break;
                }

                item = newData.CreateListViewItem(m_pType);
                m_pData.Add(newData);
                lvDataView.Items.Insert(0, item);
                lvDataView.SelectedItems.Clear();
                item.Selected = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("btnAdd_Click");
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                switch (m_pType)
                {
                    case CUIType.DataViewForm_Accounts:
                        PopulateListView(CData.SearchAccounts(tbSearch.Text));
                        break;
                    case CUIType.DataViewForm_Transactions:
                        PopulateListView(CData.SearchTransactions(tbSearch.Text));
                        break;
                    case CUIType.DataViewForm_Schedules:
                        PopulateListView(CData.SearchSchedules(tbSearch.Text));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("btnSearch_Click");
            }

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                tbSearch.Text = "";
                switch (m_pType)
                {
                    case CUIType.DataViewForm_Accounts:
                        PopulateListView(CData.SearchAccounts(tbSearch.Text));
                        break;
                    case CUIType.DataViewForm_Transactions:
                        PopulateListView(CData.SearchTransactions(tbSearch.Text));
                        break;
                    case CUIType.DataViewForm_Schedules:
                        PopulateListView(CData.SearchSchedules(tbSearch.Text));
                        break;
                    default:
                        break;
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("btnRefresh_Click");
            }
        }

        #endregion

    }
}
