using System.Threading;
using System.Threading.Tasks;
using Puya.Sms;

namespace Puya.Notification
{
    public class SmsSender
    {
        private readonly ISmsProvider sms;

        public SmsSender(ISmsProvider sms)
        {
            this.sms = sms;
        }
        public Task SendAsync(string mobile, string message)
        {
            return sms.SendAsync(new SmsSendRequest { Mobile = mobile, Message = message }, CancellationToken.None);
        }
    }
}
