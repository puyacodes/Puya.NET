using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class MemorySmsProvider : ISmsProvider
    {
        public List<SmsSendRequest> Logs = new List<SmsSendRequest>();
        public string Type => "memory";

        public Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            Logs.Add(request);

            return SmsSendResponse.Done();
        }
    }
}
