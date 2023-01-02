using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowV2.CData
{
    public abstract class CBaseData
    {
        public string m_szGuid;

        public CBaseData()
        {
            try
            {

            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #region "Override In Subclasses"
        public virtual void clear()
        {
            try
            {
                m_szGuid = Guid.NewGuid().ToString();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

    }
}
