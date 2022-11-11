using CashFlowGlobals;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowData
{
    public class CBaseData
    {
        #region "Constructors & Members"
        public int m_nID;
        public DateTime m_dtCreated;
        public DateTime m_dtModified;
        public bool m_bArchived;
        public string m_szNotes;

        [JsonIgnore]
        public ArrayList m_pUIControls;
        public CBaseData()
        {
            Clear();
        }
        #endregion

        #region "Override"
        public virtual void Clear()
        {
            m_nID = -1;
            m_dtCreated = DateTime.Now; 
            m_dtModified = DateTime.Now;
            m_pUIControls = new ArrayList();
            m_bArchived = false;
            m_szNotes = "";
        }
        public virtual void UpdateListViewItem(ref ListViewItem item)
        {
            item.SubItems.Clear();
            item.Text = "";
        }
        #endregion

        #region "Property Grid"
        [JsonIgnore]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("System")]
        [DisplayName("ID")]
        public int nID { get { return m_nID; } }
        [JsonIgnore]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("System")]
        [DisplayName("Created On")]
        public DateTime dtCreated { get { return m_dtCreated; } }
        [JsonIgnore]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("System")]
        [DisplayName("Last Modified On")]
        public DateTime dtModified { get { return m_dtModified; } }
        [JsonIgnore]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("System")]
        [DisplayName("Archived")]
        public bool bArchived { get { return bArchived; } }
        [JsonIgnore]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("Properties")]
        [DisplayName("Notes")]
        public string szNotes { get { return m_szNotes; } }
        #endregion

        #region "Does Not Change"
        public ListViewItem CreateListViewItem(CListViewType lvtype)
        {
            ListViewItem item = new ListViewItem();
            item.Tag = new CListViewTag(lvtype, this);
            UpdateListViewItem(ref item);
            m_pUIControls.Add(item);
            return item;
        }
        public void UpdateUI()
        {
            foreach(object obj in m_pUIControls)
            {
                if(obj is ListViewItem) 
                {
                    ListViewItem item = (ListViewItem)obj;
                    UpdateListViewItem(ref item); 
                }

            }
        }
        public void UpdateDateModified()
        {
            m_dtModified = DateTime.Now;
        }
        #endregion
    }
    public class CListViewTag
    {
        public CListViewType m_pListViewTypeID;
        public CBaseData m_pData;
        public CListViewTag(CListViewType tp, CBaseData data)
        {
            m_pData = data; 
            m_pListViewTypeID = tp;
        }
    }
}
