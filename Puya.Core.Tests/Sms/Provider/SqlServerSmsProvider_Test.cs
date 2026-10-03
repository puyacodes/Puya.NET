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
    public class SqlServerSmsProvider_Tests
    {
        IDb GetDb()
        {
            var constrProvider = new DefaultConnectionStringProvider();

            constrProvider.SetConnectionString("Server=.\\I2k17;Database=MyDb;User Id=sa;Password=sql2k17pass123;TrustServerCertificate=true;MultipleActiveResultSets=true");

            var db = new SqlServerDb(constrProvider);

            return db;
        }
        ISmsProvider GetSmsProvider()
        {
            return new SqlServerSmsProvider(new LogProviderBase(), GetDb(), new DateTimeNow());
        }
        [Fact]
        public async Task test_logging()
        {
            var provider = GetSmsProvider();
            var db = GetDb();

            db.ExecuteNonQuerySql("truncate table SmsLog");

            var sr = await provider.SendAsync(new SmsSendRequest { Mobile = "09123456789", Message = "this is a test" }, CancellationToken.None);

            var count = db.ExecuteScalarSql("select count(*) from SmsLog");

            Assert.True(sr.Success);
            Assert.True((int)count > 0);
        }
    }
}
