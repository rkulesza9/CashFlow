using CashFlowGlobals;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        [JsonConverter(typeof(StringEnumConverter))]
        public CAccountType m_pType;
        public string m_szName;
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
                case CUIType.DataViewForm_Accounts:
                    item.Text = m_szName;
                    item.SubItems.Add(m_pType.ToString());
                    item.SubItems.Add(m_szNotes);
                    break;
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
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
                UpdateDateModified();
                UpdateUI();
                CData.Save(CData.DB.m_szFileName);
            }
        }
        #endregion

    }
}
