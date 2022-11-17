using CashFlowGlobals;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowData
{
    public class CData
    {
        public static CJsonDatabase DB = new CJsonDatabase();

        #region "File IO"
        public static void New(string filename)
        {
            try
            {
                DB = new CJsonDatabase();
                Save(filename);
                Load(filename);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void Load(string filename)
        {
            try
            {
                string szData = File.ReadAllText(filename);
                DB = JsonConvert.DeserializeObject<CJsonDatabase>(szData);
                DB.m_szFileName = filename;
                DB.m_dtLastSaved = DateTime.Now;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void Save(string filename)
        {
            try
            {
                string szData = JsonConvert.SerializeObject(DB);
                File.WriteAllText(filename, szData);
                DB.m_szFileName = filename;
                DB.m_dtLastSaved = DateTime.Now;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        #endregion

        #region "Get Object By ID"
        public static CAccount GetAccountByID(int id)
        {
            CAccount account = new CAccount();
            try
            {
                foreach (CAccount acc in GetAccounts(true)) 
                {
                    if(acc.m_nID == id)
                    {
                        account = acc;
                        break;
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return account;
        }
        public static CTransactionSchedule GetScheduleByID(int id)
        {
            CTransactionSchedule sched = new CTransactionSchedule();
            try
            {
                foreach(CTransactionSchedule ts in GetSchedules(true))
                {
                    if(ts.m_nID == id)
                    {
                        sched = ts;
                        break;
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return sched;
        }
        #endregion

        #region "Create New Record"
        public static CAccount NewAccount()
        {
            CAccount acct = new CAccount();
            try
            {
                acct.m_nID = MaxID(DB.tblAccount)+1;
                DB.tblAccount.Add(acct);
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return acct;
        }
        public static CTransaction NewTransaction()
        {
            CTransaction tra = new CTransaction();
            try
            {
                tra.m_nID = MaxID(DB.tblTransaction)+1;
                DB.tblTransaction.Add(tra);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return tra;
        }
        public static CTransactionSchedule NewSchedule()
        {

            CTransactionSchedule ts = new CTransactionSchedule();
            try
            {
                ts.m_nID = MaxID(DB.tblTransactionSchedule)+1;
                DB.tblTransactionSchedule.Add(ts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ts;
        }
        public static int MaxID<T>(List<T> table) where T : CBaseData
        {
            int num = -1;
            foreach(CBaseData data in table)
            {
                if (num < data.m_nID) num = data.m_nID;
            }
            try
            {

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return num;
        }
        #endregion

        #region "CAccount Queries"
        public static decimal GetAccountExpectedTotalAsOf(CAccount acc, DateTime dt)
        {
            decimal result = 0M;
            try
            {
                ArrayList transactions = GetTransactions(true);
                foreach(CTransaction trans in transactions)
                {
                    if(trans.m_nAccountFromID == acc.m_nID)
                    {
                        result += -1 * trans.m_nCost;
                    } else if(trans.m_nAccountToID == acc.m_nID)
                    {
                        result += trans.m_nCost;
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }
        public static decimal GetAccountActualTotalAsOf(CAccount acc, DateTime dt)
        {
            decimal result = 0M;
            try
            {
                ArrayList transactions = GetTransactions(true);
                foreach (CTransaction trans in transactions)
                {
                    if (trans.m_nAccountFromID == acc.m_nID)
                    {
                        result += -1 * trans.m_nAmtPaid;
                    }
                    else if (trans.m_nAccountToID == acc.m_nID)
                    {
                        result += trans.m_nAmtPaid;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }
        public static ArrayList GetAccounts(bool bArchived = false, bool bDeleted = false)
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach(CAccount account in DB.tblAccount)
                {
                    bool bExitCondition = false;
                    if (!bArchived) bExitCondition = bExitCondition || account.bArchived;
                    if (!bDeleted) bExitCondition = bExitCondition || account.bDeleted;
                    if (bExitCondition) continue;
                    ls.Add(account);
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        public static ArrayList GetArchivedAccounts()
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CAccount account in DB.tblAccount)
                {
                    if (account.m_bArchived && !account.m_bDeleted) ls.Add(account);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;

        }
        public static ArrayList SearchAccounts(string szText, bool bArchived = false)
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach(CAccount acct in GetAccounts(bArchived))
                {
                    szText = szText.Trim().ToLower();
                    string[] terms = szText.Split(new char[] { ' ', ',' });
                    string json = JsonConvert.SerializeObject(acct).ToLower();
                    foreach (string term in terms)
                    {
                        if (json.Contains(term))
                        {
                            ls.Add(acct);
                            break;
                        }
                    }
                }
            }catch(Exception ex){
                Debug.WriteLine(ex);
            }
            return ls;
        }
        #endregion

        #region "CTransaction Queries"
        public static ArrayList GetTransactions(bool bArchived=false, bool bDeleted=false)
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CTransaction trans in DB.tblTransaction)
                {
                    bool bExitCondition = false;
                    if(!bArchived) bExitCondition = bExitCondition || trans.bArchived;
                    if (!bDeleted) bExitCondition = bExitCondition || trans.bDeleted;
                    if (bExitCondition) continue;

                    ls.Add(trans);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        public static ArrayList GetArchivedTransactions()
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CTransaction trans in DB.tblTransaction)
                {
                    if (trans.m_bArchived && !trans.m_bDeleted) ls.Add(trans);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;

        }
        public static bool TransactionExists(int nScheduleID, DateTime dt)
        {
            bool result = false;
            try
            {
                ArrayList transactions = GetTransactions(true);
                foreach (CTransaction trans in transactions) 
                {
                    if(trans.m_nScheduleID == nScheduleID && trans.m_dtTransaction.Equals(dt))
                    {
                        result = true;
                        break;
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }
        public static ArrayList SearchTransactions(string szText, bool bArchived=false)
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CTransaction acct in GetTransactions(bArchived))
                {
                    szText = szText.Trim().ToLower();
                    string[] terms = szText.Split(new char[] { ' ', ',' });
                    string json = JsonConvert.SerializeObject(acct).ToLower();
                    foreach (string term in terms)
                    {
                        if (json.Contains(term))
                        {
                            ls.Add(acct);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        public static DateTime GetNextTransactionDate(CTransactionSchedule sched)
        {
            DateTime dt = new DateTime();
            try
            {
                ArrayList dates = GetDates(sched, DateTime.Now, 1);
                dt = (DateTime)dates[0];
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);

            }
            return dt;
        }
        #endregion

        #region "CTransactionSchedule Queries"
        public static decimal GetScheduleExpectedTotalAsOf(CTransactionSchedule sched, DateTime dt)
        {
            decimal result = 0M;
            try
            {
                ArrayList transactions = GetTransactions(true);
                foreach (CTransaction trans in transactions)
                {
                    if(trans.m_nScheduleID == sched.m_nID) result += trans.m_nCost;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }
        public static decimal GetScheduleActualTotalAsOf(CTransactionSchedule sched, DateTime dt)
        {
            decimal result = 0M;
            try
            {
                ArrayList transactions = GetTransactions(true);
                foreach (CTransaction trans in transactions)
                {
                    if (trans.m_nScheduleID == sched.m_nID) result += trans.m_nAmtPaid;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }
        public static ArrayList GetSchedules(bool bArchived=false, bool bDeleted=false)
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CTransactionSchedule sched in DB.tblTransactionSchedule)
                {
                    bool bExitCondition = false;
                    if (!bArchived) bExitCondition = bExitCondition || sched.bArchived;
                    if (!bDeleted) bExitCondition = bExitCondition || sched.bDeleted;
                    if (bExitCondition) continue;
                    ls.Add(sched);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        public static ArrayList GetArchivedSchedules()
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CTransactionSchedule sched in DB.tblTransactionSchedule)
                {
                    if (sched.m_bArchived && !sched.m_bDeleted) ls.Add(sched);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;

        }
        public static ArrayList SearchSchedules(string szText, bool bArchived = false)
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CTransactionSchedule acct in GetSchedules(bArchived))
                {
                    szText = szText.Trim().ToLower();
                    string[] terms = szText.Split(new char[] { ' ', ',' });
                    string json = JsonConvert.SerializeObject(acct).ToLower();
                    foreach (string term in terms)
                    {
                        if (json.Contains(term))
                        {
                            ls.Add(acct);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        #endregion

        #region "Create Scheduled Transactions"
        public static ArrayList CreateScheduledTransactions(DateTime dtStart, DateTime dtEnd)
        {
            ArrayList ls = new ArrayList();
            try
            {
                ArrayList schedules = GetSchedules();
                foreach (CTransactionSchedule schedule in schedules)
                {
                    ArrayList dates = GetDates(schedule, dtStart, dtEnd);
                    foreach(DateTime dt in dates)
                    {
                        if (!TransactionExists(schedule.m_nID, dt))
                        {
                            ls.Add(CreateTransFromSched(schedule, dt));
                        } else
                        {
                            ls.Add(GetTransFromSched(schedule, dt));
                        }
                        
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        public static ArrayList GetDates(CTransactionSchedule sched, DateTime dtStart, DateTime dtEnd)
        {
            dtStart = dtStart.Date;
            dtEnd = dtEnd.Date;
            ArrayList ls = new ArrayList(); 
            try
            {
                DateTime dt = sched.m_dtStartDate;
                int nPerTimeUnit = sched.m_nPerTimeUnit;
                CTimeUnit timeUnit = sched.m_pTimeUnit;

                while (dt < dtStart) dt = Add(dt, nPerTimeUnit, timeUnit); // find dates after the start date

                while(dt < dtEnd) // find dates before end date
                {
                    ls.Add(dt);
                    dt = Add(dt, nPerTimeUnit, timeUnit);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        public static ArrayList GetDates(CTransactionSchedule sched, DateTime dtStart, int nDates)
        {
            ArrayList ls = new ArrayList();
            try
            {
                DateTime dt = sched.m_dtStartDate;
                int nPerTimeUnit = sched.m_nPerTimeUnit;
                CTimeUnit timeUnit = sched.m_pTimeUnit;

                while (dt < dtStart) dt = Add(dt, nPerTimeUnit, timeUnit); // find dates after the start date

                for(int x = 0; x < nDates; x++)// find next n dates
                {
                    ls.Add(dt);
                    dt = Add(dt, nPerTimeUnit, timeUnit);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        public static DateTime Add(DateTime dt, int n, CTimeUnit timeUnit)
        {
            DateTime result = new DateTime();
            try
            {
                switch (timeUnit)
                {
                    case CTimeUnit.Weeks:
                        result = dt.AddDays(7 * n);
                        break;
                    case CTimeUnit.Months:
                        result = dt.AddMonths(n);
                        break;
                    case CTimeUnit.Years:
                        result = dt.AddYears(n);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }
        public static CTransaction GetTransFromSched(CTransactionSchedule sched, DateTime dt)
        {
            CTransaction trans = new CTransaction();
            try
            {
                ArrayList ls = GetTransactions();
                foreach(CTransaction t in ls)
                {
                    if (t.m_nScheduleID==sched.m_nID && t.m_dtTransaction.Equals(dt))
                    {
                        trans = t;
                        break;
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return trans;
        }
        public static CTransaction CreateTransFromSched(CTransactionSchedule sched, DateTime dt)
        {
            CTransaction trans = NewTransaction();
            try
            {
                trans.m_nAccountFromID = sched.m_nAccountFromID;
                trans.m_nAccountToID = sched.m_nAccountToID;
                trans.m_nCost = sched.m_nCost;
                trans.m_nScheduleID = sched.m_nID;
                trans.m_szName = sched.m_szName;
                trans.m_szNotes = sched.m_szNotes;
                trans.m_pType = sched.m_pType;
                trans.m_dtTransaction = dt;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return trans;
        }
        #endregion
    }

    public class CJsonDatabase
    {
        public List<CAccount> tblAccount;
        public List<CTransaction> tblTransaction;
        public List<CTransactionSchedule> tblTransactionSchedule;
        public string m_szFileName;
        public DateTime m_dtLastSaved;

        public CJsonDatabase()
        {
            tblAccount = new List<CAccount>();
            tblTransaction = new List<CTransaction>();   
            tblTransactionSchedule = new List<CTransactionSchedule>();
            m_szFileName = "";
        }
    }
}
