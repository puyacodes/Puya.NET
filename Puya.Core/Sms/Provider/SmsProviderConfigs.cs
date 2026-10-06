using Puya.Extensions;
using Puya.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Puya.Sms
{
    public class SmsProviderConfigs : List<SmsProviderBaseConfig>
    {
        public SmsProviderBaseConfig GetConfig(string type)
        {
            var result = this.FirstOrDefault(x => x.Type.Equalz(type));

            return result;
        }
        public SmsProviderBaseConfig GetActive()
        {
            var result = this.FirstOrDefault(x => x.Active);

            if (result == null)
            {
                result = this.FirstOrDefault();
            }

            return result;
        }
        public string GetActiveProvider()
        {
            var active = GetActive();

            return active?.Type;
        }
        void Map<T>(SmsProviderBaseConfig source, T target)
        {
            if (source != null && source.Count > 0)
            {
                ReflectionHelper.ForEachPublicInstanceReadableNotIgnorableProperty<T>(prop =>
                {
                    if (source.ContainsKey(prop.Name))
                    {
                        try
                        {
                            if (prop.CanWrite)
                            {
                                prop.SetValue(target, source[prop.Name]);
                            }    
                        }
                        catch { }
                    }
                });
            }
        }
        public T GetConfig<T>(int i) where T : new()
        {
            var result = default(T);

            if (i >= 0 && i < Count)
            {
                var item = this[i];
                
                result = new T();

                Map(item, result);
            }

            return result;
        }
        public T GetConfig<T>(string type) where T : new()
        {
            var result = default(T);
            var item = GetConfig(type);

            if (item != null)
            {
                result = new T();

                Map(item, result);
            }

            return result;
        }
    }
}
