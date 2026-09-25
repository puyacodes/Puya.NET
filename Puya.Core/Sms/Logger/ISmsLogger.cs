using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public interface ISmsLogger
    {
        void Log(SmsLog log);
        Task LogAsync(SmsLog log, CancellationToken cancellation);
        Task<SmsLogGetPageResponse> GetPage(SmsLogGetPageRequest request, CancellationToken cancellation);
    }
}
