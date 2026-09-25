using System;
using System.Threading;
using System.Threading.Tasks;
using Puya.Logging;

namespace Puya.Sms
{
    public class DynamicSmsProvider : ISmsProvider
    {
        public DynamicSmsProvider(ISmsProviderFactory factory, ILogger logger)
        {
            Factory = factory;
            this.logger = logger;
            Type = "null";
        }
        string type;
        public virtual string Type
        {
            get {  return type; }
            set
            {
                type = value;

                if (string.IsNullOrEmpty(type))
                {
                    type = "null";
                }

                try
                {
                    instance = Factory.GetProvider(type);
                }
                catch (Exception e)
                {
                    type = "null";
                    instance = new NullSmsProvider();
                    logger.Danger(e, new { type = value });
                }
            }
        }
        ISmsProvider instance;
        public ISmsProvider Instance
        {
            get {  return instance; }
        }
        private readonly ILogger logger;
        public ISmsProviderFactory Factory { get; }
        public Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            return Instance?.SendAsync(request, cancellation);
        }
    }
}
