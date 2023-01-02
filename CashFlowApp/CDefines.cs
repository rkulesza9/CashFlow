using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowApp
{
    public class CDefines
    {
        public static readonly string JSON_DEFAULT_FILE_NAME = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\CashFlowConfig.cfm";

        public const int TYPE_TRANSACTION = 0;

        public const int UI_LISTVIEW_PROJECTS = 0;
        public const int UI_LISTVIEW_RESOURCES = 1;
        public static CColHdr[] UI_COLUMNS_PROJECTS
        {
                get {
                    return new CColHdr[]
                        {
                            new CColHdr("Name"),
                            new CColHdr("Status"),
                            new CColHdr("Last Worked On"),
                            new CColHdr("Short Note")
                        };
                }
        }

        public static CColHdr[] UI_COLUMNS_RESOURCES
        {
            get
            {
                return new CColHdr[]
                {
                    new CColHdr("Name"),
                    new CColHdr("Description")

                };
            }
        }


        public const int TRANS_TYPE_INCOME = 0;
        public const int TRANS_TYPE_BILL = 1;
        public const int TRANS_TYPE_CREDIT = 2;

        public static readonly string[] TRANS_TYPE_LABELS = new string[]
        {
            "Income",
            "Bill",
            "Credit Card"
        };

        public const int TRANS_STATUS_NEW = 0;
        public const int TRANS_STATUS_IN_PROGRESS = 1;
        public const int TRANS_STATUS_COMPLETED = 2;
        public const int TRANS_STATUS_CANCLED = 3;

        public static readonly string[] TRANS_STATUS_LABELS = new string[]
        {
            "New",
            "In Progress",
            "Completed",
            "Canceled"
        };

        public const int TRANS_TIMEPERIOD_WEEK = 0;
        public const int TRANS_TIMEPERIOD_MONTH = 1;
        public const int TRANS_TIMEPERIOD_YEAR = 2;

        public static readonly string[] TRANS_TIMEPERIOD_LABELS = new string[]
        {
            "Week",
            "Month",
            "Year"
        };

        public const string SETTINGS_LAST_OPENED_FILE = "szLastOpenedFile";

        
    }
}
