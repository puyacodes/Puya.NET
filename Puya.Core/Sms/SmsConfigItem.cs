using System.Collections.Generic;

namespace Puya.Sms
{
    public abstract class SmsConfigItem
    {
        public bool Debug { get; set; }
        public bool Active { get; set; }
        public abstract string Type { get; }
        public virtual string EndPoint { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Line { get; set; }
        public virtual Dictionary<string, string> ToDictionary()
        {
            var result = new Dictionary<string, string>();

            foreach (var prop in this.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (prop.CanRead)
                {
                    result.Add(prop.Name, prop.GetValue(this)?.ToString());
                }
            }

            return result;
        }
    }
}
