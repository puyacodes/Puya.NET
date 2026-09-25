using Newtonsoft.Json;
using Puya.Extensions;

namespace Puya.Logging
{
    public class JsonLogFormatter : ILogFormatter
    {
        public string Format(Log log)
        {
            var _log = new Log(log);
            
            _log.Data = log.GetData == null ? log.Data : log.GetData();

            return log.SafeSerialize();
        }
    }
}
