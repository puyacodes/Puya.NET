using Puya.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class SmsLoggerDebug : ISmsLogger
    {
        public Task<SmsLogGetPageResponse> GetPage(SmsLogGetPageRequest request, CancellationToken cancellation)
        {
            return Task.FromResult(new SmsLogGetPageResponse
            {
                PageCount = 0,
                RecordCount = 0,
                Items = new List<SmsLog>()
            });
        }

        public void Log(SmsLog log)
        {
            Debug.WriteLine(log.SafeSerialize());
        }

        public Task LogAsync(SmsLog log, CancellationToken cancellation)
        {
            Debug.WriteLine(log.SafeSerialize());

            return Task.CompletedTask;
        }
    }
}
