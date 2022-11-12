using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowGlobals
{
    public class CHeader : ColumnHeader
    {
        public CHeader() : base()
        {

        }

        public CHeader(string text) : base()
        {
            Text = text;
            
        }
    }
}
