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
    public class CTransaction : CBaseData
    {
        public string m_szName;
        public decimal m_nCost;
        public decimal m_nAmtPaid;
        public int m_nAccountFromID;
        public int m_nAccountToID;
        public CTransactionType m_pType;
        public int m_nScheduleID;
        public DateTime m_dtTransaction;
        public CTransaction() : base() { }

        public override void Clear()
        {
            base.Clear();
            m_szName = "";
            m_nCost = 0;
            m_nAccountFromID = -1;
            m_nAccountToID = -1;
            m_pType = CTransactionType.Bill;
            m_nScheduleID = -1;
            m_dtTransaction = DateTime.Now;
            m_nAmtPaid = 0;
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
        [DisplayName("Amount Paid")]
        public decimal nAmtPaid
        {
            get { return m_nAmtPaid; }
            set
            {
                m_nAmtPaid = value;
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
            get { return m_nAccountFromID;}
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
        [DisplayName("Type")]
        public int nScheduleID
        {
            get { return m_nScheduleID; }
            set
            {
                m_nScheduleID = value;
                UpdateUI();
                UpdateDateModified();
            }
        }

        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Transaction")]
        public DateTime dtTransaction
        {
            get { return m_dtTransaction; }
            set
            {
                m_dtTransaction = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        #endregion

    }
}
