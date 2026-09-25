using Puya.Base;
using Puya.Extensions;
using Puya.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class SmsLoggerJsonFileConfig
    {
        public string FileName { get; set; }
        public string Path { get; set; }
    }
    public class SmsLoggerJsonFile : ISmsLogger
    {
        private readonly ILogProvider logProvider;

        public SmsLoggerJsonFile(SmsLoggerJsonFileConfig config, ILogProvider logProvider)
        {
            Config = config;
            this.logProvider = logProvider;
        }
        public SmsLoggerJsonFile(ILogProvider logProvider): this(null, logProvider)
        {
        }

        public SmsLoggerJsonFileConfig Config { get; }

        public Task<SmsLogGetPageResponse> GetPage(SmsLogGetPageRequest request, CancellationToken cancellation)
        {
            var filepath = AppPath.GetFilePath(Config?.Path, Config?.FileName, "sms.log.json");
            var content = File.ReadAllText(filepath);
            var logs = content.SafeDeserialize<List<SmsLog>>() ?? new List<SmsLog>();

            if (request.Ascending)
                logs.Sort((x, y) => x.LogDate.CompareTo(y.LogDate));
            else
                logs.Sort((x, y) => y.LogDate.CompareTo(x.LogDate));

            var items = logs.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            var result = new SmsLogGetPageResponse
            {
                PageCount = request.PageSize > 0 ? logs.Count / request.PageSize : 0,
                RecordCount = logs.Count,
                Items = items.ToList()
            };

            return Task.FromResult(result);
        }

        public void Log(SmsLog log)
        {
            var filepath = AppPath.GetFilePath(Config?.Path, Config?.FileName, "sms.log.json");
            var content = File.Exists(filepath) ? File.ReadAllText(filepath): "[]";
            var logs = content.SafeDeserialize<List<SmsLog>>() ?? new List<SmsLog>();

            logs.Add(log);

            try
            {
                File.WriteAllText(filepath, logs.SafeSerialize());
            }
            catch (Exception e)
            {
                logProvider.Error("SmsLoggerJsonFile", "write failed", e);
            }
        }

        public Task LogAsync(SmsLog log, CancellationToken cancellation)
        {
            Log(log);

            return Task.CompletedTask;
        }
    }
}
