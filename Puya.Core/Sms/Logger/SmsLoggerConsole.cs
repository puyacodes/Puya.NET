using Puya.Extensions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class SmsLoggerConsole : ISmsLogger
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
            Console.WriteLine(log.SafeSerialize());
        }

        public Task LogAsync(SmsLog log, CancellationToken cancellation)
        {
            Console.WriteLine(log.SafeSerialize());

            return Task.CompletedTask;
        }
    }
}
