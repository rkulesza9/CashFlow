using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowApp
{
    public class CJsonDatabase
    {
        // json ignore
        [JsonIgnore]
        public Hashtable m_pData;
        [JsonIgnore]
        public string m_szFileName;

        //json friendly
        public List<CTransaction> m_lsTransactions;

        public CJsonDatabase() 
        {
            Initialize();
            
        }

        public void Initialize()
        {
            if(m_pData == null) m_pData = new Hashtable();
            if (m_lsTransactions == null) m_lsTransactions = new List<CTransaction>();
            if (m_szFileName == null) m_szFileName = "";
        }

        public void PopulateDataTable()
        {
            Initialize();
            m_pData.Clear();
            foreach(CTransaction trans in m_lsTransactions)
            {
                m_pData.Add(trans.szGuid, trans);
            }
        }

        public CBaseData Fetch(int nTypeID, string szGuid)
        {
            if (m_pData.ContainsKey(szGuid)) return (CBaseData) m_pData[szGuid];
            else if(szGuid == "")
            {
                return NewData(nTypeID);
            }
            else return null;
        }
        public CBaseData Fetch(int nTypeID, int nID)
        {
            if (nID == -1) return NewData(nTypeID);
            else foreach (CBaseData data in m_pData.Values) if (data.m_nTypeID==nTypeID && data.m_nID == nID) return data;

            return null;
            
        }

        public CBaseData Remove(string szGuid)
        {
            CBaseData result = (CBaseData) m_pData[szGuid];
            m_pData.Remove(szGuid);

            if (result.GetType() == typeof(CTransaction)) m_lsTransactions.Remove((CTransaction)result);

            return result;
        }

        public int NewID(int nTypeID)
        {
            int nMaxID = -1;
            int nID = -1;
            switch (nTypeID)
            {
                case CDefines.TYPE_TRANSACTION:
                    if (m_lsTransactions.Count > 0) nMaxID = m_lsTransactions.Max((trans) => { return trans.m_nID; });
                    nID = nMaxID + 1;
                    break;
                default:
                    break;
            }
            return nID;
        }

        public CBaseData NewData(int nTypeID)
        {
            CBaseData data = null;

            switch (nTypeID)
            {
                case CDefines.TYPE_TRANSACTION:
                    data = new CTransaction();
                    data.m_nID = NewID(nTypeID);
                    m_lsTransactions.Add((CTransaction)data);
                    break;
                default:
                    break;
            }

            if(data != null) m_pData.Add(data.szGuid, data);
            return data;
        }

        public List<CTransaction> GetTransactions(string szSearchTerms="", bool bActive=true, bool bArchived=false, bool bDeleted = false)
        {
            List<CTransaction> ls =  m_lsTransactions.Where((trans) =>
            {
                bool bCondition = false;
                if (bActive)
                {
                    bCondition = bCondition || (!trans.bArchived && !trans.bDeleted);
                }
                if (bArchived)
                {
                    bCondition = bCondition || (trans.bArchived);
                }
                if (bDeleted)
                {
                    bCondition = bCondition || (trans.bDeleted);
                }
                return bCondition;
            }).ToList();

            List<CTransaction> lsResults = new List<CTransaction>();
            string[] lsSearchTerms = szSearchTerms.Split(new char[] { ' ' });
            foreach(CTransaction trans in ls)
            {
                string szText = trans.szName + trans.szDescription + trans.szTransStatus + trans.szTransType + trans.szTimePeriod;
                szText = szText.ToLower();
                bool bContainsSearchTerm = true;
                foreach(string term in lsSearchTerms)
                {
                    bContainsSearchTerm = szText.Contains(term.ToLower());
                    if (!bContainsSearchTerm) break;
                }
                if (bContainsSearchTerm) lsResults.Add(trans);
            }
            return lsResults;
        }

        public bool TransactionExists(string name, DateTime dtStart)
        {
            return m_lsTransactions.Where((trans) =>
            {
                return trans.m_szName.Equals(name) && trans.m_dtStartDate.ToShortDateString().Equals(dtStart.ToShortDateString()) ;
            }).Count() > 0;
        }

        public void Save(string szFileName)
        {
            string szJson = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(szFileName, szJson);
        }

        // static objects and functions
        [JsonIgnore]
        public static CJsonDatabase Instance;
        public static void Initialize(string szFileName)
        {
            try
            {
                if (!File.Exists(szFileName)) File.Create(szFileName).Close();
                string szJson = File.ReadAllText(szFileName);
                Instance = JsonConvert.DeserializeObject<CJsonDatabase>(szJson);
                if(Instance != null) Instance.PopulateDataTable();
                else
                {
                    Instance = new CJsonDatabase();
                    Instance.PopulateDataTable();
                }
                Instance.m_szFileName = szFileName;

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        
    }
}
