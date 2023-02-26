using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowApp
{
    public class CPayPeriod
    {
        public ArrayList m_lsIncome;
        public ArrayList m_lsBills;
        public ArrayList m_lsCredit;

        public decimal m_nActIncomeTotal;
        public decimal m_nActBillsTotal;
        public decimal m_nExpIncomeTotal;
        public decimal m_nExpBillsTotal;
        public decimal m_nCreditTotal;

        public DateTime m_dtStart;
        public DateTime m_dtEnd;
        public CPayPeriod() 
        {
            Clear();
        }

        public CPayPeriod(DateTime dtStart, DateTime dtEnd, List<CTransaction> transactions)
        {
            Clear();
            PullFromMasterList(dtStart, dtEnd, transactions);
        }

        public void Clear()
        {
            m_lsIncome = new ArrayList();
            m_lsBills = new ArrayList();
            m_lsCredit = new ArrayList();
            m_nActIncomeTotal = 0;
            m_nActBillsTotal= 0;
            m_nExpIncomeTotal=0;
            m_nExpBillsTotal= 0;
            m_nCreditTotal= 0;
            m_dtStart = DateTime.Now.AddDays(-7);
            m_dtEnd = DateTime.Now.AddDays(7);
        }
        public void PullFromMasterList(DateTime dtStart, DateTime dtEnd, List<CTransaction> transactions)
        {
            dtStart = DateTime.Parse(dtStart.ToShortDateString());
            dtEnd = DateTime.Parse(dtEnd.ToShortDateString());

            m_dtStart = dtStart;
            m_dtEnd = dtEnd;

            int x = 0;
            foreach(CTransaction trans in transactions)
            {
                if (trans.m_nParentID != CDefines.DEFAULT_ID)
                {
                    x++;
                    Debug.WriteLine($"{x}");
                    if (dtStart <= trans.m_dtStartDate && trans.m_dtStartDate <= dtEnd)
                    {
                        AddToList(trans);
                    }
                } 
                else
                {
                    DateTime dtFirstDateAfter = FirstDateAfter(trans.m_dtStartDate, dtStart, trans.m_nTimesPerPeriod, trans.m_nTimePeriodID);
                    DateTime dtLastDateBefore = LastDateBefore(trans.m_dtStartDate, dtEnd, trans.m_nTimesPerPeriod, trans.m_nTimePeriodID);

                    if (dtFirstDateAfter.Equals(new DateTime()) || dtLastDateBefore.Equals(new DateTime())) continue;

                    while(dtFirstDateAfter <= dtLastDateBefore)
                    {
                        CTransaction pExistingTrans = CJsonDatabase.Instance.GetNonRecurringTransaction(trans.m_nID, dtFirstDateAfter);
                        if (pExistingTrans == null)
                        {
                            CTransaction trans1 = CreateTransaction(trans, dtFirstDateAfter);
                            AddToList(trans1);
                        } else
                        {
                            AddToList(pExistingTrans);
                        }

                        dtFirstDateAfter = IncrementDate(dtFirstDateAfter, trans.m_nTimesPerPeriod, trans.m_nTimePeriodID);
                    }
                }

            }

            UpdateTotals();
        }

        public CTransaction CreateTransaction(CTransaction trans, DateTime dt)
        {
            CTransaction trans1 = (CTransaction) CJsonDatabase.Instance.Fetch(CDefines.TYPE_TRANSACTION, "");
            trans1.m_szName = trans.m_szName;
            trans1.m_szDescription = trans.m_szDescription;
            trans1.m_nTransTypeID = trans.m_nTransTypeID;
            trans1.m_nTransStatusID = trans.m_nTransStatusID;
            trans1.m_bArchived = trans.m_bArchived;
            trans1.m_bDeleted = trans.m_bDeleted;
            trans1.m_nCost = trans.m_nCost;
            //m_nTimesPerPeriod = trans.m_nTimesPerPeriod;
            //m_nTimePeriodID = trans.m_nTimePeriodID;
            trans1.m_dtStartDate = dt;
            trans1.m_nParentID = trans.m_nID;
            CJsonDatabase.Instance.Save(CJsonDatabase.Instance.m_szFileName);

            return trans1;
        } 
        public DateTime FirstDateAfter(DateTime dt, DateTime dtAfter, int nPerTimeUnit, int nTimePeriodID)
        {
            while(dt <= dtAfter)
            {                
                dt = IncrementDate(dt, nPerTimeUnit, nTimePeriodID);
                
            }

            return dt;
        }
        public DateTime LastDateBefore(DateTime dt, DateTime dtBefore, int nPerTimeUnit, int nTimePeriodID)
        {
            DateTime dtLast = new DateTime();
            while(dt <= dtBefore)
            {
                dtLast = dt;
                dt = IncrementDate(dt, nPerTimeUnit, nTimePeriodID);
            }

            return dtLast;
        }
        public DateTime IncrementDate(DateTime dt, int nPerTimeUnit, int nTimePeriodID)
        {
            DateTime dtResult = new DateTime();
            switch (nTimePeriodID)
            {
                case CDefines.TRANS_TIMEPERIOD_WEEK:
                    dtResult = dt.AddDays(7* nPerTimeUnit);
                    break;
                case CDefines.TRANS_TIMEPERIOD_MONTH:
                    dtResult = dt.AddMonths(1* nPerTimeUnit);
                    break;
                case CDefines.TRANS_TIMEPERIOD_YEAR:
                    dtResult = dt.AddYears(1* nPerTimeUnit);
                    break;
                default:
                    break;
            }
            return dtResult;
        }
        public void AddToList(CTransaction trans)
        {
            switch (trans.m_nTransTypeID)
            {
                case CDefines.TRANS_TYPE_INCOME:
                    m_lsIncome.Add(trans);
                    break;
                case CDefines.TRANS_TYPE_BILL:
                    m_lsBills.Add(trans);
                    break;
                case CDefines.TRANS_TYPE_CREDIT:
                    m_lsCredit.Add(trans);
                    break;
                default:
                    break;
            }
        }
        public void UpdateTotals()
        {
            m_nActIncomeTotal = 0;
            m_nExpIncomeTotal = 0;
            foreach(CTransaction trans in m_lsIncome)
            {
                m_nActIncomeTotal += trans.m_nCost;
                if(trans.m_nTransStatusID == CDefines.TRANS_STATUS_COMPLETED)
                {
                    m_nExpIncomeTotal += trans.m_nCost;
                }
            }

            m_nActBillsTotal = 0;
            m_nExpBillsTotal = 0;
            foreach(CTransaction trans in m_lsBills)
            {
                m_nActBillsTotal += trans.m_nCost;
                if (trans.m_nTransStatusID == CDefines.TRANS_STATUS_COMPLETED)
                {
                    m_nExpBillsTotal += trans.m_nCost;
                }
            }

            m_nCreditTotal = 0;
            foreach(CTransaction trans in m_lsCredit)
            {
                m_nCreditTotal += trans.m_nCost;
            }
        }

        public void PrintSummary()
        {
            Debug.WriteLine("Summary ----- ");
            Debug.WriteLine($"m_dtStart: {m_dtStart}");
            Debug.WriteLine($"m_dtEnd: {m_dtEnd}");
            Debug.WriteLine($"m_lsIncome: {m_lsIncome.Count}");
            Debug.WriteLine($"m_lsBills: {m_lsBills.Count}");
            Debug.WriteLine($"m_lsCredit: {m_lsCredit.Count}");
            Debug.WriteLine($"m_nActIncomeTotal: {m_nActIncomeTotal}");
            Debug.WriteLine($"m_nActBillsTotal: {m_nActBillsTotal}");
            Debug.WriteLine($"m_nExpIncomeTotal: {m_nExpIncomeTotal}");
            Debug.WriteLine($"m_nExpBillsTotal: {m_nExpBillsTotal}");
            Debug.WriteLine($"m_nCreditTotal: {m_nCreditTotal}");
            Debug.WriteLine("Summary ----- ");
        }

    }
}
