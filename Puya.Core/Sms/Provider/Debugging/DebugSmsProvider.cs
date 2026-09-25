using Puya.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class DebugSmsProvider: ISmsProvider
    {
        public string Type => "debug";

        public Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.FFFFF"));
            System.Diagnostics.Debug.WriteLine(request.SafeSerialize());
            System.Diagnostics.Debug.WriteLine("");

            return SmsSendResponse.Done();
        }
    }
}
