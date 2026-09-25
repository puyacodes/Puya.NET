using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class SmsLoggerNull : ISmsLogger
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
        }

        public Task LogAsync(SmsLog log, CancellationToken cancellation)
        {
            return Task.CompletedTask;
        }
    }
}
