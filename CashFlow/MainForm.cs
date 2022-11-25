using CashFlowData;
using CashFlowGlobals;
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

namespace CashFlow
{
    public partial class MainForm : Form
    { 
        public string m_szFile;
        public DataViewForm m_dvfm;
        public PayPeriodView m_payPeriod;
        public ArchiveForm m_archive;
        public MainForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            m_szFile = "";

            string szStartMsg = "Wait to recieve bank notification before updating this app.";
            string szStartTitle = "Important";
            MessageBox.Show(szStartMsg, szStartTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region "Event Listener"
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog pDialog = new SaveFileDialog();
                pDialog.Filter = "cfm files (*.cfm)|*.cfm|all files (*.*)|*.*";
                pDialog.Title = "Create New File";

                if (pDialog.ShowDialog(this) == DialogResult.OK)
                {
                    m_szFile = pDialog.FileName;
                    lblFileName.Text = m_szFile;
                    lblLastSaveDate.Text = DateTime.Now.ToString();
                    CData.New(m_szFile);

                    // Create scheduled transactions
                    //CData.CreateScheduledTransactions(DateTime.Now, DateTime.Now.AddMonths(1));

                    // open accounts form
                    if (!(m_dvfm is null) && !m_dvfm.IsDisposed) m_dvfm.Close();
                    m_dvfm = new DataViewForm();
                    m_dvfm.m_pType = CUIType.ListView_Accounts;
                    m_dvfm.m_szFilename = m_szFile;
                    m_dvfm.Initialize(this);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("newToolStripMenuItem_Click");
                Debug.WriteLine(ex);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog pDialog = new OpenFileDialog();
                pDialog.Filter = "cfm files (*.cfm)|*.cfm|all files (*.*)|*.*";
                pDialog.Title = "Open File";

                if (pDialog.ShowDialog(this) == DialogResult.OK)
                {
                    m_szFile = pDialog.FileName;
                    lblFileName.Text = m_szFile;
                    lblLastSaveDate.Text = DateTime.Now.ToString();
                    CData.Load(m_szFile);

                    // Create scheduled transactions
                    //CData.CreateScheduledTransactions(DateTime.Now, DateTime.Now.AddMonths(1));

                    // open accounts form
                    if (!(m_dvfm is null) && !m_dvfm.IsDisposed) m_dvfm.Close();
                    m_dvfm = new DataViewForm();
                    m_dvfm.m_pType = CUIType.ListView_Accounts;
                    m_dvfm.m_szFilename = m_szFile;
                    m_dvfm.Initialize(this);
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("openToolStripMenuItem_Click");
                Debug.WriteLine(ex);
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CData.Save(m_szFile);
                lblLastSaveDate.Text = DateTime.Now.ToString();
                MessageBox.Show("File Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show("saveToolStripMenuItem_Click");
                Debug.WriteLine(ex);
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog pDialog = new SaveFileDialog();
                pDialog.Filter = "cfm files (*.cfm)|*.cfm|all files (*.*)|*.*";
                pDialog.Title = "Save File";

                if (pDialog.ShowDialog(this) == DialogResult.OK)
                {
                    m_szFile = pDialog.FileName;
                    lblFileName.Text = m_szFile;
                    lblLastSaveDate.Text = DateTime.Now.ToString();
                    CData.Save(m_szFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("saveAsToolStripMenuItem_Click");
                Debug.WriteLine(ex);
            }

        }
        #endregion

        public void UpdateLastSavedDate()
        {
            try
            {
                lblLastSaveDate.Text = DateTime.Now.ToString();

            }
            catch(Exception ex)
            {
                MessageBox.Show("UpdateLastSavedDate");
                Debug.WriteLine(ex);
            }
        }
        private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(m_dvfm is null) && !m_dvfm.IsDisposed) m_dvfm.Close();

                m_dvfm = new DataViewForm();
                m_dvfm.m_pType = CUIType.ListView_Accounts;
                m_dvfm.m_szFilename = m_szFile;
                m_dvfm.Initialize(this);
            }catch(Exception ex)
            {
                MessageBox.Show("accountsToolStripMenuItem_Click");
                Debug.WriteLine(ex);
            }
        }

        private void scheduledTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(m_dvfm is null) && !m_dvfm.IsDisposed) m_dvfm.Close();
                m_dvfm = new DataViewForm();
                m_dvfm.m_pType = CUIType.ListView_Schedules;
                m_dvfm.m_szFilename = m_szFile;
                m_dvfm.Initialize(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show("scheduledTransactionsToolStripMenuItem_Click");
                Debug.WriteLine(ex);
            }

        }

        private void transactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(m_dvfm is null) && !m_dvfm.IsDisposed) m_dvfm.Close();
                m_dvfm = new DataViewForm();
                m_dvfm.m_pType = CUIType.ListView_Transactions;
                m_dvfm.m_szFilename = m_szFile;
                m_dvfm.Initialize(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show("transactionsToolStripMenuItem_Click");
                Debug.WriteLine(ex);
            }
        }

        private void payPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(m_payPeriod is null || m_payPeriod.IsDisposed)
                {
                    m_payPeriod = new PayPeriodView();
                }
                m_payPeriod.MdiParent = this;
                m_payPeriod.Show();
                m_payPeriod.BringToFront();

            }catch(Exception ex)
            {
                MessageBox.Show("payPeriodToolStripMenuItem_Click");
                Debug.WriteLine(ex);

            }
        }

        private void accountsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(m_archive is null) && !m_archive.IsDisposed) m_archive.Close();
                m_archive = new ArchiveForm();
                m_archive.m_pType = CUIType.ListView_Accounts;
                m_archive.m_szFilename = m_szFile;
                m_archive.Initialize(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show("accountsToolStripMenuItem1_Click");
                Debug.WriteLine(ex);
            }
        }

        private void scheduledTransactionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(m_archive is null) && !m_archive.IsDisposed) m_archive.Close();
                m_archive = new ArchiveForm();
                m_archive.m_pType = CUIType.ListView_Schedules;
                m_archive.m_szFilename = m_szFile;
                m_archive.Initialize(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("scheduledTransactionsToolStripMenuItem1_Click");
                Debug.WriteLine(ex);
            }
        }

        private void transactionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(m_archive is null) && !m_archive.IsDisposed) m_archive.Close();
                m_archive = new ArchiveForm();
                m_archive.m_pType = CUIType.ListView_Transactions;
                m_archive.m_szFilename = m_szFile;
                m_archive.Initialize(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show("transactionsToolStripMenuItem1_Click");
                Debug.WriteLine(ex);
            }
        }
    }
}
