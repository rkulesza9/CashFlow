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
        public static CJsonDatabase DB;

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
            }catch(Exception ex)
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
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }
        #endregion

        #region "Get Object By ID"
        public CAccount GetAccountByID(int id)
        {
            CAccount account = new CAccount();
            try
            {
                foreach(CAccount acc in DB.tblAccount)
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
        public CTransactionSchedule GetScheduleByID(int id)
        {
            CTransactionSchedule sched = new CTransactionSchedule();
            try
            {
                foreach(CTransactionSchedule ts in DB.tblTransactionSchedule)
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
                acct.m_nID = MaxID(DB.tblAccount);
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
                tra.m_nID = MaxID(DB.tblTransaction);
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
                ts.m_nID = MaxID(DB.tblTransactionSchedule);
                DB.tblTransactionSchedule.Add(ts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ts;
        }
        public static int MaxID(ArrayList table)
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
        public static ArrayList GetAccounts()
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach(CAccount account in DB.tblAccount)
                {
                    if (account.bArchived) continue;
                    ls.Add(account);
                }
            }catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return ls;
        }
        #endregion

        #region "CTransaction Queries"
        public static ArrayList GetTransactions()
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CTransaction trans in DB.tblTransaction)
                {
                    if (trans.bArchived) continue;
                    ls.Add(trans);
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
                ArrayList transactions = GetTransactions();
                foreach (CTransaction trans in transactions) 
                {
                    if(trans.nID == nScheduleID && trans.m_dtTransaction.Equals(dt))
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
        #endregion

        #region "CTransactionSchedule Queries"
        public static ArrayList GetSchedules()
        {
            ArrayList ls = new ArrayList();
            try
            {
                foreach (CTransactionSchedule sched in DB.tblTransactionSchedule)
                {
                    if (sched.bArchived) continue;
                    ls.Add(sched);
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
        public static void CreateScheduledTransactions(DateTime dtStart, DateTime dtEnd)
        {
            try
            {
                ArrayList schedules = GetSchedules();
                foreach (CTransactionSchedule schedule in schedules)
                {
                    ArrayList dates = GetDates(schedule, dtStart, dtEnd);
                    foreach(DateTime dt in dates)
                    {
                        if (!TransactionExists(schedule.m_nID, dt)) CreateTransFromSched(schedule, dt);
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public static ArrayList GetDates(CTransactionSchedule sched, DateTime dtStart, DateTime dtEnd)
        {
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
        public ArrayList tblAccount;
        public ArrayList tblTransaction;
        public ArrayList tblTransactionSchedule;

        public CJsonDatabase()
        {
            tblAccount = new ArrayList();
            tblTransaction = new ArrayList();   
            tblTransactionSchedule = new ArrayList();
        }
    }
}
