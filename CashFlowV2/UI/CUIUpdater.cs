using CashFlowV2.CData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowV2.UI
{
    public class CUIUpdater
    {
        public static Hashtable m_pControls = new Hashtable();

        public static void Register(CBaseData data, object control)
        {
            if(m_pControls.ContainsKey(data))
            {
                ArrayList ls = (ArrayList)m_pControls[data];
                ls.Add(control);
            }else
            {
                ArrayList ls = new ArrayList();
                ls.Add(control);
                m_pControls[data] = ls;                
            }
        }

        public static void Unregister(CBaseData data, object control)
        {
            if (m_pControls.ContainsKey(data))
            {
                ArrayList ls = (ArrayList)m_pControls[data];
                ls.Remove(control);
            }
        }

        public static void Unregister(CListViewItem item)
        {
            Unregister(item.m_pData, item);
        }
    }
}
