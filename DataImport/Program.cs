using CashFlowData;
using CashFlowGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImport
{
    internal class Program
    {
        const int NID_EXTERNAL = 0;
        const int NID_FAFCU_BILLS = 1;
        const int NID_FAFCU_SPEND = 2;
        const int NID_FAFCU_SAVINGS = 3;
        const int NID_FAFCU_CREDIT = 4;

        static void Main(string[] args)
        {
            string filename = "E:\\NextCloud\\Records\\TestFinancesFile.cfm";
            CData.DB = new CJsonDatabase();
            AddAccounts(ref CData.DB);
            AddTransScheds(ref CData.DB);

            CData.Save(filename);
        }

        static void AddAccounts(ref CJsonDatabase db)
        {
            // db.tblAccount;

            db.tblAccount.Add(CreateAccount(NID_EXTERNAL, CAccountType.External, "EXTERNAL SOURCE"));
            db.tblAccount.Add(CreateAccount(NID_FAFCU_BILLS, CAccountType.Debit, "FAFCU - BILLS"));
            db.tblAccount.Add(CreateAccount(NID_FAFCU_SPEND, CAccountType.Debit, "FAFCU - SPEND"));
            db.tblAccount.Add(CreateAccount(NID_FAFCU_SAVINGS, CAccountType.Savings, "FAFCU - SAVINGS"));
            db.tblAccount.Add(CreateAccount(NID_FAFCU_CREDIT, CAccountType.Credit, "FAFCU - CREDIT"));
        }

        static void AddTransScheds(ref CJsonDatabase db)
        {
            int nID = 0;

            // db.tblTransactionSchedule;

            // BILLS
            db.tblTransactionSchedule.Add(
                CreateTransSched(++nID, "Amazon Prime", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("12/5/2022"), 15.98M,
                                 1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
                CreateTransSched(++nID, "Credit Card Debt", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/9/2022"), 300M,
                                 2, CTimeUnit.Weeks, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
                CreateTransSched(++nID, "Crunchyroll", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/18/2022"), 9.99M,
                                 1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
                CreateTransSched(++nID, "Every Plate", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/9/2022"), 72M,
                                 1, CTimeUnit.Weeks, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Ferret Cleaner", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("2/3/2023"), 10M,
                             6, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Ferret Food", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("12/21/2022"), 36M,
                             6, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Haircut", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/20/2022"), 65M,
                             3, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Hulu", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/23/2022"), 19M,
                             1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Laundry", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("12/1/2022"), 40M,
                             1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Misfit Market", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/9/2022"), 50M,
                             1, CTimeUnit.Weeks, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Njalla", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("1/26/2023"), 16.02M,
                             3, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Noip - Domain", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("10/17/2023"), 36M,
                             1, CTimeUnit.Years, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Noip - Dynamic DNS", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("10/13/2023"), 24.95M,
                             1, CTimeUnit.Years, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Noip - Plus DNS", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("10/17/2023"), 29.95M,
                             1, CTimeUnit.Years, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Peacock", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("12/5/2022"), 4.99M,
                             1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Fafcu Loan", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/18/2022"), 376.81M,
                             1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Proton Mail", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("7/11/2023"), 47.88M,
                             1, CTimeUnit.Years, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Rent", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("12/1/2022"), 820M,
                             1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Renter's Insurance", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/15/2022"), 12.42M,
                             1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Spotify", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/17/2022"), 10.65M,
                             1, CTimeUnit.Months, CTransactionType.Bill));
            db.tblTransactionSchedule.Add(
            CreateTransSched(++nID, "Utilities", NID_FAFCU_BILLS, NID_EXTERNAL, DateTime.Parse("11/14/2022"), 200M,
                             1, CTimeUnit.Months, CTransactionType.Bill));


        }

        #region "Create DB Object"
        static CAccount CreateAccount(int nID, CAccountType type, string name)
        {
            CAccount spend = new CAccount();
            spend.m_nID = nID;
            spend.m_pType = type;
            spend.m_szName = name;
            return spend;
        }
        static CTransactionSchedule CreateTransSched(int nID, string name, int AccountFromID, int AccountToID,
            DateTime StartDate, decimal cost, int nPerTimeUnit, CTimeUnit timeUnit, CTransactionType type)
        {
            CTransactionSchedule sched = new CTransactionSchedule();
            sched.m_nID = nID;
            sched.m_szName = name;
            sched.m_nAccountFromID = AccountFromID;
            sched.m_nAccountToID = AccountToID;
            sched.m_dtStartDate = StartDate;
            sched.m_nCost = cost;
            sched.m_nPerTimeUnit = nPerTimeUnit;
            sched.m_pTimeUnit = timeUnit;
            sched.m_pType = type;
            return sched;
        }
        #endregion
    }
}
