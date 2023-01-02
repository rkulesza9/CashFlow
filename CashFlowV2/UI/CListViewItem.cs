using CashFlowV2.CData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowV2.UI
{
    public class CListViewItem : ListViewItem
    {
        public int m_nListViewTypeID;
        public CBaseData m_pData;
        public CListViewItem() : base() 
        {
            try
            {
                m_nListViewTypeID = -1;
                m_pData = null;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public CListViewItem(CBaseData pData, int nListViewType) : base()
        {
            try
            {
                m_pData = pData;
                m_nListViewTypeID = nListViewType;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void Update()
        {
            switch (m_nListViewTypeID)
            {
                case CDefines.UI_LISTVIEW_BILL:
                    break;
                case CDefines.UI_LISTVIEW_INCOME:
                    break;
                case CDefines.UI_LISTVIEW_PAYPERIOD:
                    break;
                default:
                    break;
            }
        }
        
    }
}
