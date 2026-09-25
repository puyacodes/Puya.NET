using Puya.Collections;
using Puya.Extensions;
using Puya.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Puya.Sms
{
    public class SmsProviderConfigs : List<DynamicStringModel>
    {
        public DynamicStringModel GetConfig(string type)
        {
            var result = this.FirstOrDefault(x => x["Type"].Equalz(type));

            return result;
        }
        public DynamicStringModel GetActive()
        {
            var result = this.FirstOrDefault(x => x["Active"].Equalz("true"));

            if (result == null)
            {
                result = this.FirstOrDefault();

                if (result == null)
                {
                    result = new DynamicStringModel();
                }
            }

            return result;
        }
        void Map<T>(DynamicStringModel source, T target)
        {
            if (source != null && source.Count > 0)
            {
                ReflectionHelper.ForEachPublicInstanceReadableNotIgnorableProperty<T>(prop =>
                {
                    if (source.ContainsKey(prop.Name))
                    {
                        try
                        {
                            prop.SetValue(target, source[prop.Name]);
                        }
                        catch { }
                    }
                });
            }
        }
        public T GetConfig<T>(int i) where T : new()
        {
            var result = new T();

            if (i < 0 || i >= Count)
            {
                var item = this[i];

                Map(item, result);
            }

            return result;
        }
        public T GetConfig<T>(string type) where T : new()
        {
            var result = new T();
            var item = GetConfig(type);

            Map(item, result);

            return result;
        }
    }
}
