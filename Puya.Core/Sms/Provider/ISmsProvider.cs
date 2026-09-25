using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public interface ISmsProvider
    {
        string Type { get; }
        Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation);
    }
}
