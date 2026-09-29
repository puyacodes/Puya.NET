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
    public class SmsLoggerSqlServer_Tests
    {
        IDb GetDb()
        {
            var constrProvider = new DefaultConnectionStringProvider();

            constrProvider.SetConnectionString("Server=.\\I2k17;Database=MyDb;User Id=sa;Password=sql2k17pass123;TrustServerCertificate=true;MultipleActiveResultSets=true");

            var db = new SqlServerDb(constrProvider);

            return db;
        }
        ISmsLogger GetSmsLogger()
        {
            return new SmsLoggerSqlServer(new LogProviderBase(), GetDb(), new DateTimeNow());
        }
        [Fact]
        public void test_logging()
        {
            var logger = GetSmsLogger();
            var db = GetDb();

            db.ExecuteNonQuerySql("truncate table SmsLog");

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

            var count = db.ExecuteScalarSql("select count(*) from SmsLog");

            Assert.True((int)count > 0);
        }
        [Fact]
        public async Task test_getpage()
        {
            var logger = GetSmsLogger();
            var db = GetDb();

            db.ExecuteNonQuerySql("truncate table SmsLog");

            for (var i = 0; i < 50; i++)
            {
                logger.Log(new SmsLog
                {
                    Category = "personnel",
                    Data = new { a = i },
                    LineNo = "10002000",
                    Message = $"this is message {i}",
                    Provider = "db",
                    RefCode = "101",
                    Response = i % 6 == 0 ? "{success:false}" : "{success:true}",
                    Status = i % 6 == 0 ? "failed" : "success",
                    Topic = "send"
                });
            }

            var r = await logger.GetPage(new SmsLogGetPageRequest { Page = 1, PageSize = 10, OrderBy = "Serial" }, CancellationToken.None);

            Assert.NotNull(r);
            Assert.True(r.RecordCount == 50);
            Assert.True(r.PageCount == 5);
            Assert.NotNull(r.Items);
            Assert.True(r.Items.Count == 10);
            Assert.NotNull(r.Items[0]);
            Assert.True(r.Items[0].Message == $"this is message {49}");
        }
    }
}
