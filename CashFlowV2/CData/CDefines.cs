using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowV2.CData
{
    public class CDefines
    {
        // Data Type
        public const int TYPE_INCOME = 0;
        public const int TYPE_BILL = 1;
        public const int TYPE_PAYPERIOD = 2;

        // List View Type
        public const int UI_LISTVIEW_INCOME = 0;
        public const int UI_LISTVIEW_BILL = 1;
        public const int UI_LISTVIEW_PAYPERIOD = 2;

    }
    public enum CTimeUnit
    {
        Weeks,
        Months,
        Years
    }
}
