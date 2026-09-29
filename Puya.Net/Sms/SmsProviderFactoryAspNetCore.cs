using Microsoft.Extensions.DependencyInjection;
using Puya.Data;
using Puya.Date;
using Puya.Service;
using System;

namespace Puya.Sms
{
    public class SmsProviderFactoryAspNetCore : ISmsProviderFactory
    {
        protected readonly IServiceProvider serviceProvider;

        public SmsProviderFactoryAspNetCore(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public virtual ISmsProvider GetProvider(string type)
        {
            var db = serviceProvider.GetService<IDb>();
            var now = serviceProvider.GetService<INow>();
            var logProvider = serviceProvider.GetService<ILogProvider>();

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
                case "sqlserver":
                    return new SqlServerSmsProvider(logProvider, db, now);
                default:
                    throw new System.Exception($"Sms provider type '{type}' is not supported.");
            }
        }
    }
}