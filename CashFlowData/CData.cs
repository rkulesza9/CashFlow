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

        #region "Get Foreign Keys"
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

        public static void CreateScheduledTransactions(DateTime EndDate)
        {
            try
            {
                foreach(CTransactionSchedule sched in DB.tblTransactionSchedule)
                {
                    DateTime dt = sched.m_dtStartDate;
                    while(dt < EndDate)
                    {

                        switch (sched.m_pTimeUnit)
                        {
                            case CTimeUnit.Weeks:
                                dt = dt.AddDays(7);
                                break;
                            case CTimeUnit.Months:
                                break;
                            case CTimeUnit.Years:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
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
