using Hangfire;
using Puya.Collections;
using Puya.Sms;
using System;
using System.Threading;

namespace Puya.Notification
{
    public class HangFireSmsNotifier : ISmsNotifier
    {
        private readonly ISmsProvider sms;
        private readonly IBackgroundJobClient backgroundJobClient;

        public HangFireSmsNotifier(ISmsProvider sms, IBackgroundJobClient backgroundJobClient)
        {
            this.sms = sms;
            this.backgroundJobClient = backgroundJobClient;
        }
        public void Notify(string target, string message)
        {
            var mobiles = target?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var mobile in mobiles)
            {
                backgroundJobClient.Enqueue(() => sms.SendAsync(new SmsSendRequest { Mobile = mobile, Message = message }, CancellationToken.None));
            }
        }

        public void Notify(string target, string templateCode, DynamicModel parameters, object otherData = null)
        {
            var mobiles = target?.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var mobile in mobiles)
            {
                backgroundJobClient.Enqueue(() => sms.SendAsync(new SmsSendRequest { Mobile = mobile, TemplateCode = templateCode, Parameters = parameters, OtherData = otherData }, CancellationToken.None));
            }
        }
    }
}
