using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowApp
{
    public class CListViewComparer : IComparer
    {
        public int m_nListViewTypeID;
        public int m_nColumn;
        public SortOrder m_pSortOrder;
        public CListViewComparer(int nListViewTypeID, int nColumn, SortOrder pOrder)
        {
            m_nListViewTypeID = nListViewTypeID;
            m_nColumn = nColumn;
            m_pSortOrder = pOrder;
        }

        public int Compare(object x, object y)
        {
            CBaseData xData = GetData(x);
            CBaseData yData = GetData(y);
            CTransaction xTrans, yTrans;
            //CResource xRes, yRes;
            int nOrder = m_pSortOrder == SortOrder.Ascending ? 1 : -1;

            // pinned should always appear on top
            if (xData.m_bPinned && !yData.m_bPinned) return -1;
            if (yData.m_bPinned && !xData.m_bPinned) return 1;

            switch (m_nListViewTypeID)
            {
                case CDefines.UI_LISTVIEW_TRANS:
                    xTrans = (CTransaction)xData;
                    yTrans = (CTransaction)yData;
                    if (m_nColumn == 0) return nOrder * xTrans.m_szName.CompareTo(yTrans.m_szName);
                    if (m_nColumn == 1) return nOrder * xTrans.szTransStatus.CompareTo(yTrans.szTransStatus);
                    if (m_nColumn == 2) return nOrder * xTrans.szTransType.CompareTo(yTrans.szTransType);
                    if (m_nColumn == 3) return nOrder * xTrans.m_szDescription.CompareTo(yTrans.m_szDescription);
                    if (m_nColumn == 4) return nOrder * xTrans.m_nCost.CompareTo(yTrans.m_nCost);
                    if (m_nColumn == 5) return nOrder * xTrans.m_nTimesPerPeriod.CompareTo(yTrans.m_nTimesPerPeriod);
                    if (m_nColumn == 6) return nOrder * xTrans.szTimePeriod.CompareTo(yTrans.szTimePeriod);
                    break;
                default:
                    break;
            }

            return -1;
        }

        public CBaseData GetData(object x)
        {
            CListViewItem item = (CListViewItem)x;
            CBaseData data = (CBaseData)item.Tag;
            return data;
        }
    }
}
