using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowGlobals
{
    public class CConstants
    {
        #region "ListView Columns"
        public readonly static CHeader[] COLUMNS_ACCOUNT =
        {
            new CHeader("Name"),
            new CHeader("Type"),
            new CHeader("Expected Total"),
            new CHeader("Actual Total"),
            new CHeader("Notes")
        };
        public readonly static CHeader[] COLUMNS_TRANSACTION =
        {
            new CHeader("Name"),
            new CHeader("From"),
            new CHeader("To"),
            new CHeader("Cost"),
            new CHeader("Amt Paid"),
            new CHeader("Date")
        };
        public readonly static CHeader[] COLUMNS_SCHEDULE =
        {
            new CHeader("Name"),
            new CHeader("From"),
            new CHeader("To"),
            new CHeader("Cost"),
            new CHeader("Next Date"),
            new CHeader("Expected Total"),
            new CHeader("Actual Total"),
            new CHeader("Notes")
        };
        #endregion

    }
}
