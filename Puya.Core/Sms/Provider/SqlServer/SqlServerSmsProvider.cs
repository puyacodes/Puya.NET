using Puya.Data;
using Puya.Date;
using Puya.Extensions;
using Puya.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class SqlServerSmsProvider : ISmsProvider
    {
        private readonly ISmsLogger logger;
        public SqlServerSmsProvider(ILogProvider logProvider, IDb db, INow now)
        {
            logger = new SmsLoggerSqlServer(logProvider, db, now);
        }
        public string Type => "sqlserver";

        public async Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            var result = null as SmsSendResponse;
            var log = new SmsLog
            {
                Category = request.Category,
                MobileNo = request.Mobile,
                Message = request.Message,
                Provider = Type,
                Success = true,
                Status = "Success",
                Data = new { request.TemplateCode, request.Parameters, request.OtherData },
            };

            try
            {
                await logger?.LogAsync(log, cancellation);

                result = await SmsSendResponse.Done();
            }
            catch (Exception e)
            {
                result = new SmsSendResponse();
                result.Failed(e);
            }

            return result;
        }
    }
}
