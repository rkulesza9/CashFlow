using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach(CTransaction trans in transactions)
            {
                ArrayList ls = trans.CreateTransactions(dtStart, dtEnd);
                foreach(CTransaction trans1 in ls)
                {
                    switch (trans1.m_nTypeID)
                    {
                        case CDefines.TRANS_TYPE_BILL:
                            m_lsBills.Add(trans1);
                            break;
                        case CDefines.TRANS_TYPE_INCOME:
                            m_lsIncome.Add(trans1);
                            break;
                        case CDefines.TRANS_TYPE_CREDIT:
                            m_lsCredit.Add(trans1);
                            break;
                        default:
                            break;
                    }
                }
                
            }

            UpdateTotals();
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

    }
}
