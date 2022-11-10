using CashFlowData;
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
        public MainForm()
        {
            InitializeComponent();
            m_szFile = "";
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
                    CData.New(m_szFile);
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
                    CData.Load(m_szFile);
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

    }
}
