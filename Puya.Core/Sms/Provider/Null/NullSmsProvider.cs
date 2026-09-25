using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class NullSmsProvider : ISmsProvider
    {
        public string Type => "null";

        public Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            return SmsSendResponse.Done();
        }
    }
}
