using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowGlobals
{
    public enum CAccountType
    {
        Debit,
        Credit,
        Savings,
        Loan,
        External
    }
    public enum CTransactionType
    {
        Spend,
        Bill,
        Income,
        Credit
    }
    public enum CTimeUnit
    {
        Weeks,
        Months,
        Years
    }
}
