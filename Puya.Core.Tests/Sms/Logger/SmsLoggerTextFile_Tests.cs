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
    public class SmsLoggerTetFile_Tests
    {
        SmsLoggerTextFile GetSmsLogger()
        {
            return new SmsLoggerTextFile(new SmsLoggerTextFileConfig { }, new LogProviderBase());
        }
        [Fact]
        public void test_logging()
        {
            var logger = GetSmsLogger();
            var filepath = AppPath.GetFilePath(logger.Config?.Path, logger.Config?.FileName, "sms.log.txt");

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            logger.Log(new SmsLog
            {
                Category = "personnel",
                Data = new { a = 10 },
                LineNo = "10002000",
                Message = "this is a message",
                Provider = "db",
                RefCode = "101",
                Response = "{success:true}",
                Status = "success",
                Topic = "send"
            });

            var lines = File.ReadAllLines(filepath);
            
            Assert.NotNull(lines);
            Assert.True(lines.Length > 0);
        }
    }
}
