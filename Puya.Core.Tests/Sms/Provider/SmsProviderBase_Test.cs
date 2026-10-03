using Puya.Base;
using Puya.Data;
using Puya.Date;
using Puya.Debugging;
using Puya.Logging;
using Puya.Service;
using Puya.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Puya.Core.Tests.Sms
{
    public class MySmsProviderConfig { }
    public class MySmsProvider : SmsProviderBase<MySmsProviderConfig>
    {
        public MySmsProvider(MySmsProviderConfig config, ISmsLogger logger, IDebugger debugger, ILogProvider logProvider, ISmsTemplateCodeAdaptor<string> templateCodeAdaptor) : base(config, logger, debugger, logProvider, templateCodeAdaptor)
        {
        }

        public override string Type => "my";

        protected override Task<SmsSendResponse> SendInternalAsync(SmsSendRequest request, SmsLog log, CancellationToken cancellation)
        {
            return SmsSendResponse.Done();
        }
    }
    public class SmsProviderBase_Tests
    {
        MySmsProvider GetSmsProvider()
        {
            return new MySmsProvider(new MySmsProviderConfig { },
                                    new SmsLoggerMemory(),
                                    new ManualDebugger { IsDebugging = true },
                                    new LogProviderBase(),
                                    new PassThroughSmsTemplateCodeAdaptor());
        }
        [Fact]
        public async Task test_logging()
        {
            var provider = GetSmsProvider();
            
            var sr = await provider.SendAsync(new SmsSendRequest { Mobile = "09123456789", Message = "this is a test" }, CancellationToken.None);

            Assert.True(sr.Success);
            Assert.True(((SmsLoggerMemory)provider.Logger).Logs.Count > 0);
        }
    }
}
