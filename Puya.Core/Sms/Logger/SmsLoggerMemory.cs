using Puya.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class SmsLoggerMemory : ISmsLogger
    {
        public List<SmsLog> Logs { get; private set; }
        public SmsLoggerMemory()
        {
            Logs = new List<SmsLog>();
        }
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
            Logs.Add(log);
        }

        public Task LogAsync(SmsLog log, CancellationToken cancellation)
        {
            Logs.Add(log);

            return Task.CompletedTask;
        }
    }
}
