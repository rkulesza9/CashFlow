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
            CBaseData xData = (CBaseData)x;
            CBaseData yData = (CBaseData)y;
            int nSort = m_pOrder == SortOrder.Ascending ? 1 : -1;

            switch (m_pType)
            {
                case CUIType.DataViewForm_Accounts:
                    break;
                case CUIType.DataViewForm_Schedules:
                    break;
                case CUIType.DataViewForm_Transactions:
                    break;
                default:
                    break;
            }
            return -1;
        }
    }
}
