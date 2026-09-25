using Puya.Extensions;

namespace Puya.Logging
{
    public class JsonLogDataConverter : ILogDataConverter
    {
        public object Deserialize(string data)
        {
            return data.SafeDeserialize();
        }
        public string Serialize(object data)
        {
            return data.SafeSerialize();
        }
    }
}
