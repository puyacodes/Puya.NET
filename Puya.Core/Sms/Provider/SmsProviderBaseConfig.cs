using Puya.Collections;
using Puya.Conversion;
using System.Collections.Generic;

namespace Puya.Sms
{
    public class SmsProviderBaseConfig: DynamicModel
    {
        public SmsProviderBaseConfig()
        { }
        public SmsProviderBaseConfig(IDictionary<string, object> dic): base(dic)
        { }
        public SmsProviderBaseConfig(IEnumerable<KeyValuePair<string, object>> items): base(items)
        { }
        public bool Active
        {
            get { return SafeClrConvert.ToBoolean(this["Active"]); }
            set { this["Active"] = value; }
        }
        public string Type
        {
            get { return this["Type"]?.ToString(); }
            set { this["Type"] = value; }
        }
    }
}
