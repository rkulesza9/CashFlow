using CashFlowGlobals;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public bool m_bDeleted;
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
            m_bDeleted = false;
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
        public bool bArchived 
        { 
            get { return m_bArchived; } 
            set 
            { 
                m_bArchived = value;
                CData.Save(CData.DB.m_szFileName);
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [ReadOnly(true)]
        [Category("System")]
        [DisplayName("Deleted")]
        public bool bDeleted 
        { 
            get { return m_bDeleted; }
            set
            {
                m_bDeleted = value;
                CData.Save(CData.DB.m_szFileName);
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Notes")]
        public string szNotes 
        { 
            get { return m_szNotes; } 
            set 
            { 
                m_szNotes = value;
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
            } 
        }
        #endregion

        #region "Does Not Change"
        public ListViewItem CreateListViewItem(CUIType lvtype)
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
        public CUIType m_pListViewTypeID;
        public CBaseData m_pData;
        public CListViewTag(CUIType tp, CBaseData data)
        {
            m_pData = data; 
            m_pListViewTypeID = tp;
        }
    }
}
