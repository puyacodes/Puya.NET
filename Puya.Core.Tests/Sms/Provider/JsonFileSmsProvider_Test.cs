using Puya.Base;
using Puya.Data;
using Puya.Date;
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
    public class JsonFileSmsProvider_Tests
    {
        JsonFileSmsProvider GetSmsProvider()
        {
            return new JsonFileSmsProvider(new JsonFileSmsProviderConfig { FileName = "sms.out.json" }, new DateTimeNow());
        }
        [Fact]
        public async Task test_logging()
        {
            var provider = GetSmsProvider();
            var filepath = AppPath.GetFilePath(provider.Config?.Path, provider.Config?.FileName, "sms.out.json");

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            var sr = await provider.SendAsync(new SmsSendRequest { Mobile = "09123456789", Message = "this is a test" }, CancellationToken.None);

            var lines = File.ReadAllLines(filepath);

            Assert.True(sr.Success);
            Assert.NotNull(lines);
            Assert.True(lines.Length > 0);
        }
    }
}
