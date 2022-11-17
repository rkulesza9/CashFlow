using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowGlobals
{
    public class CListViewColumns
    {
        public static CHeader[] GetColumnsFor(CUIType lvType)
        {
            switch (lvType)
            {
                case CUIType.ListView_Schedules:
                    return new CHeader[]
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
                case CUIType.ListView_Transactions:
                    return new CHeader[]
                    {
                        new CHeader("Name"),
                        new CHeader("From"),
                        new CHeader("To"),
                        new CHeader("Cost"),
                        new CHeader("Amt Paid"),
                        new CHeader("Date")
                    };
                case CUIType.ListView_Accounts:
                    return new CHeader[]
                    {
                        new CHeader("Name"),
                        new CHeader("Type"),
                        new CHeader("Expected Total"),
                        new CHeader("Actual Total"),
                        new CHeader("Notes")
                    };
                default:
                    return new CHeader[] { };

            }
        }
    }
}
