using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowApp
{
    public class CTransaction : CBaseData
    {
        public string m_szName;
        public string m_szDescription;
        public int m_nTransTypeID;
        public int m_nTransStatusID;
        public bool m_bArchived;
        public bool m_bDeleted; 
        public decimal m_nCost;
        public int m_nTimesPerPeriod;
        public int m_nTimePeriodID;
        
        public CTransaction() : base() { }
        
        #region "override me"
        public override void Clear()
        {
            base.Clear();
            try
            {
                m_nTypeID = CDefines.TYPE_TRANSACTION;
                m_szName = "";
                m_szDescription = "";
                m_nTransTypeID = CDefines.TRANS_TYPE_BILL;
                m_nTransStatusID = CDefines.TRANS_STATUS_NEW;
                m_bArchived = false;
                m_bDeleted = false;
                m_nCost = 0;
                m_nTimesPerPeriod = 0;
                m_nTimePeriodID = CDefines.TRANS_TIMEPERIOD_MONTH;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion
        public override void UpdateListViewItem(ref CListViewItem item)
        {
            try
            {
                item.Text = "";
                item.SubItems.Clear();

                switch (item.m_nListViewTypeID)
                {
                    default:
                        break;
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #region "Property Grid"
        [JsonIgnore]
        [ReadOnly(false)]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Name")]
        public string szName
        {
            get
            {
                return m_szName;
            }
            set
            {
                m_szName = value;
                PropertyUpdate();
            }
        }
        [JsonIgnore]
        [ReadOnly(false)]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Description")]
        public string szDescription
        {
            get
            {
                return m_szDescription;
            }
            set
            {
                m_szDescription= value;
                PropertyUpdate();
            }
        }
        [JsonIgnore]
        [ReadOnly(false)]
        [Browsable(true)]
        [Category("Properties")]
        [TypeConverter(typeof(CTypeConverters.CTransTypeLabelConverter))]
        [DisplayName("Type")]
        public string szTransType
        {
            get
            {
                return CDefines.TRANS_TYPE_LABELS[m_nTransTypeID];
            }
            set
            {
                m_nTransTypeID = GetLabelID(value, CDefines.TRANS_TYPE_LABELS);
                PropertyUpdate();
            }
        }

        [JsonIgnore]
        [Browsable(false)]
        public int nTransTypeID
        {
            get
            {
                return m_nTransTypeID;
            }
            set
            {
                m_nTransTypeID= value;
                PropertyUpdate();
            }
        }

        [JsonIgnore]
        [ReadOnly(false)]
        [Browsable(true)]
        [Category("Properties")]
        [TypeConverter(typeof(CTypeConverters.CTransStatusLabelConverter))]
        [DisplayName("Status")]
        public string szTransStatus
        {
            get
            {
                return CDefines.TRANS_STATUS_LABELS[m_nTransStatusID];
            }
            set
            {
                m_nTransStatusID = GetLabelID(value, CDefines.TRANS_STATUS_LABELS);
                PropertyUpdate();
            }
        }
        [JsonIgnore]
        [Browsable(false)]
        public int nTransStatusID
        {
            get
            {
                return m_nTransStatusID;
            }
            set
            {
                m_nTransStatusID = value;
                PropertyUpdate();
            }
        }

        [JsonIgnore]
        [ReadOnly(true)]
        [Browsable(true)]
        [Category("System")]
        [DisplayName("Archived")]
        public bool bArchived
        {
            get
            {
                return m_bArchived;
            }
            set
            {
                m_bArchived = value;
                PropertyUpdate();
            }
        }

        [JsonIgnore]
        [ReadOnly(true)]
        [Browsable(true)]
        [Category("System")]
        [DisplayName("Deleted")]
        public bool bDeleted
        {
            get
            {
                return m_bDeleted;
            }
            set
            {
                m_bDeleted = value;
                PropertyUpdate();
            }
        }

        [JsonIgnore]
        [ReadOnly(false)]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Cost")]
        [TypeConverter(typeof(CTypeConverters.CMoneyConverter))]
        public decimal nCost
        {
            get
            {
                return m_nCost;
            }
            set
            {
                m_nCost = value;
                PropertyUpdate();
            }
        }
        [JsonIgnore]
        [ReadOnly(false)]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Times Per Period")]
        public int nTimesPerPeriod
        {
            get
            {
                return m_nTimesPerPeriod;
            }
            set
            {
                m_nTimesPerPeriod = value;
                PropertyUpdate();
            }
        }
        [JsonIgnore]
        [ReadOnly(false)]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Time Period")]
        [TypeConverter(typeof(CTypeConverters.CTimePeriodConverter))]
        public string szTimePeriod
        {
            get
            {
                return CDefines.TRANS_TIMEPERIOD_LABELS[m_nTimePeriodID];
            }
            set
            {
                m_nTimePeriodID = GetLabelID(value, CDefines.TRANS_TIMEPERIOD_LABELS);
                PropertyUpdate();
            }
        }
        [JsonIgnore]
        [Browsable(false)]
        public int nTimePeriodID
        {
            get
            {
                return m_nTimePeriodID;
            }
            set
            {
                m_nTimePeriodID = value;
                PropertyUpdate();
            }
        }
        #endregion
    }
}
