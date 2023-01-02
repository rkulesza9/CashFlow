using Newtonsoft.Json;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CashFlowApp
{
    public partial class FmMain : Form
    {
        public ArrayList m_pOpenForms;
        public FmMain()
        {
            InitializeComponent();
            try
            {
                CJsonDatabase.Initialize(GetLastFile());
                UpdateStatusBar(CJsonDatabase.Instance.m_szFileName, DateTime.Now);
                m_pOpenForms = new ArrayList();

                OpenForm(new FmTransaction());
            }
            catch(Exception ex)
            {
                MessageBox.Show("MainForm()");
                Debug.WriteLine(ex);
            }
        }

        #region "Events"
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog pDialog = new SaveFileDialog();
                pDialog.Filter = "cfm files (*.cfm)|*.cfm|all files (*.*)|*.*";
                pDialog.Title = "Create New File";

                if(pDialog.ShowDialog() == DialogResult.OK)
                {
                    CJsonDatabase.Initialize(pDialog.FileName);
                    UpdateStatusBar(pDialog.FileName, DateTime.Now);
                    Properties.Settings.Default[CDefines.SETTINGS_LAST_OPENED_FILE] = pDialog.FileName;
                    Properties.Settings.Default.Save();

                    CloseOpenForms();
                    OpenForm(new FmTransaction());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnNew_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CJsonDatabase.Instance.Save(CJsonDatabase.Instance.m_szFileName);
                UpdateStatusBar(CJsonDatabase.Instance.m_szFileName, DateTime.Now);
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnSave_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog pDialog = new OpenFileDialog();
                pDialog.Filter = "cfm files (*.cfm)|*.cfm|all files (*.*)|*.*";
                pDialog.Title = "Open File";

                if (pDialog.ShowDialog() == DialogResult.OK)
                {
                    CJsonDatabase.Initialize(pDialog.FileName);
                    UpdateStatusBar(pDialog.FileName, DateTime.Now);
                    Properties.Settings.Default[CDefines.SETTINGS_LAST_OPENED_FILE] = pDialog.FileName;
                    Properties.Settings.Default.Save();

                    CloseOpenForms();
                    OpenForm(new FmTransaction());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("btnOpen_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog pDialog = new SaveFileDialog();
                pDialog.Filter = "cfm files (*.cfm)|*.cfm|all files (*.*)|*.*";
                pDialog.Title = "Save File";

                if (pDialog.ShowDialog() == DialogResult.OK)
                {
                    CJsonDatabase.Instance.m_szFileName = pDialog.FileName;
                    CJsonDatabase.Instance.Save(pDialog.FileName);
                    UpdateStatusBar(pDialog.FileName, DateTime.Now);
                    Properties.Settings.Default[CDefines.SETTINGS_LAST_OPENED_FILE] = pDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnSaveAs_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(new FmTransaction());
            }
            catch (Exception ex)
            {
                MessageBox.Show("btnTransactions_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnPayPeriod_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("btnPayPeriod_Click");
                Debug.WriteLine(ex);
            }
        }

        private void btnArchivedTrans_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(new FmArchivedTrans());

            }
            catch (Exception ex)
            {
                MessageBox.Show("btnArchivedTrans_Click");
                Debug.WriteLine(ex);
            }
        }
        private void deletedTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenForm(new FmDeletedTrans());

            }
            catch (Exception ex)
            {
                MessageBox.Show("deletedTransactionsToolStripMenuItem_Click");
                Debug.WriteLine(ex);
            }

        }
        #endregion

        public string GetLastFile()
        {
            string szFileName = (string) Properties.Settings.Default[CDefines.SETTINGS_LAST_OPENED_FILE];
            if (szFileName != null && !szFileName.Equals(""))
            {
                return szFileName;
            } else
            {
                Properties.Settings.Default[CDefines.SETTINGS_LAST_OPENED_FILE] = CDefines.JSON_DEFAULT_FILE_NAME;
            }
            return CDefines.JSON_DEFAULT_FILE_NAME;
        }
        public void UpdateStatusBar(string filename, DateTime dtLastSaved)
        {
            lblFilename.Text = filename;
            lblLastSave.Text = dtLastSaved.ToString();
        }

        public void OpenForm(Form fm)
        {
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();

            m_pOpenForms.Add(fm);
        }
        public void CloseOpenForms()
        {
            foreach(Form fm in m_pOpenForms)
            {
                fm.Close();
            }

            m_pOpenForms.Clear();
        }

    }
}
