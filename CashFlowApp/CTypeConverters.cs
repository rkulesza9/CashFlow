using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowApp
{
    public class CTypeConverters
    {
        public class CTransTypeLabelConverter : StringConverter
        {
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				//true means show a combobox
				return true;
			}
			//----------------------------------------------------------------------------
			public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
			{
				//true will limit to list. false will show the list, but allow free-form entry
				return true;
			}
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
				return new StandardValuesCollection(CDefines.TRANS_TYPE_LABELS);
            }
        }
        public class CTransStatusLabelConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                //true means show a combobox
                return true;
            }
            //----------------------------------------------------------------------------
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                //true will limit to list. false will show the list, but allow free-form entry
                return true;
            }
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(CDefines.TRANS_STATUS_LABELS);
            }
        }
        public class CTimePeriodConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                //true means show a combobox
                return true;
            }
            //----------------------------------------------------------------------------
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                //true will limit to list. false will show the list, but allow free-form entry
                return true;
            }
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(CDefines.TRANS_TIMEPERIOD_LABELS);
            }
        }
        public class CMoneyConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                string str = (string)value;
                decimal d = decimal.MinValue;
                decimal.TryParse(str, out d);
                if (d != decimal.MinValue) return d;
                return base.ConvertFrom(context, culture, value);
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                decimal dec = (decimal)value;
                string str = dec.ToString("C");
                return str;
            }
        }
    }
}
