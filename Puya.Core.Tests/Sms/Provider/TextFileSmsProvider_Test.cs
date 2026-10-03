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
    public class TextFileSmsProvider_Tests
    {
        TextFileSmsProvider GetSmsProvider()
        {
            return new TextFileSmsProvider(new TextFileSmsProviderConfig { FileName = "sms.out.txt" }, new DateTimeNow());
        }
        [Fact]
        public async Task test_logging()
        {
            var provider = GetSmsProvider();
            var filepath = AppPath.GetFilePath(provider.Config?.Path, provider.Config?.FileName, "sms.out.txt");

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            await provider.SendAsync(new SmsSendRequest { Mobile = "09123456789", Message = "this is a test" }, CancellationToken.None);

            var lines = File.ReadAllLines(filepath);

            Assert.NotNull(lines);
            Assert.True(lines.Length > 0);
        }
    }
}
