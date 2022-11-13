using CashFlowGlobals;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        [JsonConverter(typeof(StringEnumConverter))]
        public CTransactionType m_pType;
        public int m_nPerTimeUnit;
        [JsonConverter(typeof(StringEnumConverter))]
        public CTimeUnit m_pTimeUnit;
        public DateTime m_dtStartDate;
        public bool m_bActive;

        public CTransactionSchedule() : base() { }

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
            m_bActive = true;
        }

        public override void UpdateListViewItem(ref ListViewItem item)
        {
            base.UpdateListViewItem(ref item);
            CListViewTag tag = item.Tag as CListViewTag;
            switch (tag.m_pListViewTypeID)
            {
                case CUIType.DataViewForm_Schedules:
                    item.Text = m_szName;
                    item.SubItems.Add(CData.GetAccountByID(m_nAccountFromID).m_szName);
                    item.SubItems.Add(CData.GetAccountByID(m_nAccountToID).m_szName);
                    item.SubItems.Add(m_nCost.ToString("c"));
                    item.SubItems.Add(CData.GetNextTransactionDate(this).ToShortDateString());
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
        [DisplayName("From")]
        public int nAccountFromID
        {
            get { return m_nAccountFromID; }
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
        [DisplayName("Start Date")]
        public DateTime dtStartDate
        {
            get { return m_dtStartDate; }
            set
            {
                m_dtStartDate = value;
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Active")]
        public bool bActive
        {
            get { return m_bActive; }
            set
            {
                m_bActive = value;
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);

            }
        }
        #endregion
    }
}
