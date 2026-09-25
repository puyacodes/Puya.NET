using Puya.Date;
using Puya.Service;

namespace Puya.Sms
{
    public class SmsProviderFactoryDefault : ISmsProviderFactory
    {
        private readonly ISmsLogger logger;
        private readonly INow now;

        public SmsProviderFactoryDefault(ISmsLogger logger, INow now)
        {
            this.logger = logger;
            this.now = now;
        }
        public virtual ISmsProvider GetProvider(string type)
        {
            switch (type)
            {
                case "console":
                    return new ConsoleSmsProvider();
                case "debug":
                    return new DebugSmsProvider();
                case null:
                case "":
                case "null":
                    return new NullSmsProvider();
                case "textfile":
                    return new TextFileSmsProvider(new TextFileSmsProviderConfig(), now);
                case "jsonfile":
                    return new JsonFileSmsProvider(new JsonFileSmsProviderConfig(), now);
                case "memory":
                    return new MemorySmsProvider();
                case "db":
                    return new DbSmsProvider(logger);
                default:
                    throw new System.Exception($"Sms provider type '{type}' is not supported.");
            }
        }
    }
}