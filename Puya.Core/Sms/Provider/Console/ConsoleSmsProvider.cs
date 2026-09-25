using Puya.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class ConsoleSmsProvider: ISmsProvider
    {
        public string Type => "console";

        public Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.FFFFF"));
            Console.WriteLine(request.SafeSerialize());
            Console.WriteLine();

            return SmsSendResponse.Done();
        }
    }
}
