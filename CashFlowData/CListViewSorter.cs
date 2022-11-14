using CashFlowGlobals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowData
{
    public class CListViewSorter : IComparer
    {
        public int m_nColumn;
        public SortOrder m_pOrder;
        public CUIType m_pType;
        public CListViewSorter(CUIType lvType, int nCol, SortOrder order)
        {
            m_nColumn = nCol;
            m_pOrder = order;
            m_pType = lvType;
        }
        public int Compare(object x, object y)
        {
            ListViewItem xItem = (ListViewItem)x;
            ListViewItem yItem = (ListViewItem)y;
            CListViewTag xTag = (CListViewTag)xItem.Tag;
            CListViewTag yTag = (CListViewTag)(yItem.Tag);
            CBaseData xData = xTag.m_pData;
            CBaseData yData = yTag.m_pData;

            int nSort = m_pOrder == SortOrder.Ascending ? 1 : -1;
            DateTime dt = DateTime.Now;
            DateTime xdtNext;
            DateTime ydtNext;
            CAccount xAcc;
            CAccount yAcc;
            CAccount xAcc2;
            CAccount yAcc2;
            CTransactionSchedule xSched;
            CTransactionSchedule ySched;
            CTransaction xTrans;
            CTransaction yTrans;

            switch (m_pType)
            {
                case CUIType.DataViewForm_Accounts:
                    xAcc = (CAccount)xData;
                    yAcc = (CAccount)yData;
                    if(m_nColumn == 0) return nSort * xAcc.m_szName.CompareTo(yAcc.m_szName);
                    if(m_nColumn == 1) return nSort * xAcc.m_pType.CompareTo(yAcc.m_pType);
                    if (m_nColumn == 2) return nSort * Expected(xAcc, dt).CompareTo(Expected(yAcc, dt));
                    if(m_nColumn == 3) return nSort * Actual(xAcc, dt).CompareTo(Actual(yAcc, dt));
                    if(m_nColumn == 4) return nSort * xAcc.m_szNotes.CompareTo(yAcc.m_szNotes);
                    break;
                case CUIType.DataViewForm_Schedules:
                    xSched = (CTransactionSchedule)xData;
                    ySched = (CTransactionSchedule)yData;
                    xAcc = CData.GetAccountByID(xSched.m_nAccountFromID);
                    yAcc = CData.GetAccountByID(ySched.m_nAccountFromID);
                    xAcc2 = CData.GetAccountByID(xSched.m_nAccountToID);
                    yAcc2 = CData.GetAccountByID(ySched.m_nAccountToID);
                    xdtNext = CData.GetNextTransactionDate(xSched);
                    ydtNext = CData.GetNextTransactionDate(ySched);
                    if(m_nColumn == 0) return nSort * xSched.m_szName.CompareTo(ySched.m_szName);
                    if (m_nColumn == 1) return nSort * xAcc.m_szName.CompareTo(yAcc.m_szName);
                    if (m_nColumn == 2) return nSort * xAcc2.m_szName.CompareTo(yAcc2.m_szName);
                    if(m_nColumn == 3) return nSort * xSched.m_nCost.CompareTo(ySched.m_nCost);
                    if(m_nColumn == 4) return nSort * xdtNext.CompareTo(ydtNext);
                    if (m_nColumn == 5) return nSort * Expected(xSched, dt).CompareTo(Expected(ySched, dt));
                    if (m_nColumn == 6) return nSort * Actual(xSched, dt).CompareTo(Actual(ySched, dt));
                    if (m_nColumn == 7) return nSort * xSched.m_szNotes.CompareTo(ySched.m_szNotes);
                    break;
                case CUIType.DataViewForm_Transactions:
                    xTrans = (CTransaction)xData;
                    yTrans = (CTransaction)yData;
                    xAcc = CData.GetAccountByID(xTrans.m_nAccountFromID);
                    yAcc = CData.GetAccountByID(yTrans.m_nAccountFromID);
                    xAcc2 = CData.GetAccountByID(xTrans.m_nAccountToID);
                    yAcc2 = CData.GetAccountByID(yTrans.m_nAccountToID);
                    if(m_nColumn == 0) return nSort * xTrans.m_szName.CompareTo(yTrans.m_szName);
                    if (m_nColumn == 1) return nSort * xAcc.m_szName.CompareTo(yAcc.m_szName);
                    if (m_nColumn == 2) return nSort * xAcc2.m_szName.CompareTo(yAcc2.m_szName);
                    if (m_nColumn == 3) return nSort * xTrans.m_nCost.CompareTo(yTrans.m_nCost);
                    if (m_nColumn == 4) return nSort * xTrans.m_nAmtPaid.CompareTo(yTrans.m_nAmtPaid);
                    if (m_nColumn == 5) return nSort * xTrans.m_dtTransaction.CompareTo(yTrans.m_dtTransaction);
                    break;
                default:
                    break;
            }
            return -1;
        }

        private decimal Actual(CBaseData x, DateTime dt)
        {
            if(m_pType == CUIType.DataViewForm_Accounts)
            {
                CAccount xAcc = (CAccount)x;
                return CData.GetAccountActualTotalAsOf(xAcc, dt);
            } else
            {
                CTransactionSchedule xSched = (CTransactionSchedule)x;
                return CData.GetScheduleActualTotalAsOf(xSched, dt);
            }
        }
        private decimal Expected(CBaseData x, DateTime dt)
        {
            if (m_pType == CUIType.DataViewForm_Accounts)
            {
                CAccount xAcc = (CAccount)x;
                return CData.GetAccountExpectedTotalAsOf(xAcc, dt);
            }
            else
            {
                CTransactionSchedule xSched = (CTransactionSchedule)x;
                return CData.GetScheduleExpectedTotalAsOf(xSched, dt);
            }
        }
    }
}
