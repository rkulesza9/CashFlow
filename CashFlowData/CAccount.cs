using CashFlowGlobals;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashFlowData
{
    public class CAccount : CBaseData
    {
        public string m_szName;
        public CAccountType m_pType;
        public CAccount() : base() { }

        public override void Clear()
        {
            base.Clear();
            m_szName = "";
            m_pType = CAccountType.External;
        }
        public override void UpdateListViewItem(ref ListViewItem item)
        {
            base.UpdateListViewItem(ref item);
            CListViewTag tag = item.Tag as CListViewTag;
            switch (tag.m_pListViewTypeID)
            {
                default:
                    break;
            }
        }

        #region "Property Grid"
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Name")]
        public string szName
        {
            get { return m_szName; }
            set
            {
                m_szName = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        [JsonIgnore]
        [Browsable(true)]
        [Category("Properties")]
        [DisplayName("Type")]
        public CAccountType pType
        {
            get { return m_pType; }
            set
            {
                m_pType = value;
                UpdateUI();
                UpdateDateModified();
            }
        }
        #endregion

    }
}
