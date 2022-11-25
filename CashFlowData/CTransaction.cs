using CashFlowGlobals;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        [JsonConverter(typeof(StringEnumConverter))]
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
                case CUIType.ListView_Transactions:
                    item.Text = m_szName;                    
                    item.SubItems.Add(CData.GetAccountByID(m_nAccountFromID).m_szName);
                    item.SubItems.Add(CData.GetAccountByID(m_nAccountToID).m_szName);
                    item.SubItems.Add(m_nCost.ToString("c"));
                    item.SubItems.Add(m_nAmtPaid.ToString("c"));
                    item.SubItems.Add(m_dtTransaction.ToShortDateString());
                    break;
                case CUIType.PayPeriodView_ListView:
                    item.Text = m_szName;

                    if (m_nAmtPaid == m_nCost) item.BackColor = Color.LightGreen;
                    else if (m_nAmtPaid > 0 && m_nAmtPaid < m_nCost) item.BackColor = Color.LightYellow;

                    item.SubItems.Add(m_dtTransaction.ToShortDateString());
                    item.SubItems.Add(m_nCost.ToString("c"));
                    item.SubItems.Add(m_nAmtPaid.ToString("c"));
                    break;
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Schedule ID")]
        public int nScheduleID
        {
            get { return m_nScheduleID; }
            set
            {
                m_nScheduleID = value;
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
            }
        }
        #endregion

    }
}
