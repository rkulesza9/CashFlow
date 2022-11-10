using CashFlowGlobals;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowData
{
    public class CTransactionSchedule : CBaseData
    {
        public string m_szName;
        public decimal m_nCost;
        public int m_nAccountFromID;
        public int m_nAccountToID;
        public CTransactionType m_pType;
        public int m_nPerTimeUnit;
        public CTimeUnit m_pTimeUnit;
        public DateTime m_dtStartDate;

        public CTransactionSchedule() : base() {  }

        public override void Clear()
        {
            base.Clear();
            m_szName = "";
            m_nCost = 0;
            m_nAccountFromID = -1;
            m_nAccountToID = -1;
            m_pType = CTransactionType.Bill;
            m_nPerTimeUnit = 1;
            m_pTimeUnit = CTimeUnit.Months;
            m_dtStartDate = DateTime.Now;
        }

        public override void UpdateListViewItem(ref ListViewItem item)
        {
            base.UpdateListViewItem(ref item);
            CListViewTag tag = item.Tag as CListViewTag;
            switch (tag.m_pListViewTypeID)
            {
                default:
                    break;
            }
        }

        #region "Property Grid"
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Name")]
        public string szName
        {
            get { return m_szName; }
            set
            {
                m_szName = value;
                UpdateUI();
                UpdateDateModified();

            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Cost")]
        public decimal nCost
        {
            get { return m_nCost; }
            set
            {
                m_nCost = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("From")]
        public int nAccountFromID
        {
            get { return m_nAccountFromID; }
            set
            {
                m_nAccountFromID = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("To")]
        public int nAccountToID
        {
            get { return m_nAccountToID; }
            set
            {
                m_nAccountToID = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Type")]
        public CTransactionType pType
        {
            get { return m_pType; }
            set
            {
                m_pType = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Start Date")]
        public DateTime dtStartDate
        {
            get { return m_dtStartDate; }
            set
            {
                m_dtStartDate = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Per Time Unit")]
        public int nPerTimeUnit
        {
            get { return m_nPerTimeUnit; }
            set
            {
                m_nPerTimeUnit = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Time Unit")]
        public CTimeUnit pTimeUnit
        {
            get { return m_pTimeUnit; }
            set
            {
                m_pTimeUnit = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        #endregion
    }
}
