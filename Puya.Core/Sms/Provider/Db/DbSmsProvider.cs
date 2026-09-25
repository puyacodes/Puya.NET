using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class DbSmsProvider : ISmsProvider
    {
        private readonly ISmsLogger logger;

        public DbSmsProvider(ISmsLogger logger)
        {
            this.logger = logger;
        }
        public string Type => "db";

        public async Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
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

            await logger?.LogAsync(log, cancellation);

            return await SmsSendResponse.Done();
        }
    }
}
