using Puya.Base;
using Puya.Extensions;
using Puya.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class SmsLoggerTextFileConfig
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string LineSeparator { get; set; }
    }
    public class SmsLoggerTextFile : ISmsLogger
    {
        private readonly ILogProvider logProvider;

        public SmsLoggerTextFile(SmsLoggerTextFileConfig config, ILogProvider logProvider)
        {
            Config = config;
            this.logProvider = logProvider;
        }
        public SmsLoggerTextFile(ILogProvider logProvider): this(null, logProvider)
        { }

        public SmsLoggerTextFileConfig Config { get; }
        string GetLineSeparator()
        {
            return string.IsNullOrEmpty(Config?.LineSeparator) ? new string('-', 50) : Config.LineSeparator;
        }
        public Task<SmsLogGetPageResponse> GetPage(SmsLogGetPageRequest request, CancellationToken cancellation)
        {
            /* implementation not complete.
             * it cannot handle log.Response, log.Data properly
             * 
            var filepath = AppPath.GetFilePath(Config?.Path, Config?.FileName, "sms.log.txt");
            var lines = File.ReadAllLines(filepath);
            var props = ReflectionHelper.GetPublicInstanceReadableProperties<SmsLog>();
            var logs = new List<SmsLog>();
            var log = new SmsLog();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                
                if (line == GetLineSeparator())
                {
                    logs.Add(log);
                    log = new SmsLog();
                    continue;
                }

                foreach (var prop in props)
                {
                    if (line.StartzWith(prop.Name + ":"))
                    {
                        var value = line.Substring(prop.Name.Length + 1).Trim();

                        if (prop.PropertyType == typeof(DateTime))
                        {
                            if (DateTime.TryParse(value, out var dt))
                                prop.SetValue(log, dt);
                        }
                        else if (prop.PropertyType == typeof(int))
                        {
                            if (int.TryParse(value, out var i))
                                prop.SetValue(log, i);
                        }
                        else if (prop.PropertyType == typeof(bool))
                        {
                            if (bool.TryParse(value, out var b))
                                prop.SetValue(log, b);
                        }
                        else
                        {
                            prop.SetValue(log, value);
                        }
                    }
                }
            }

            if (request.Ascending)
                logs.Sort((x, y) => x.LogDate.CompareTo(y.LogDate));
            else
                logs.Sort((x, y) => y.LogDate.CompareTo(x.LogDate));

            var items = logs.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);

            var result = new SmsLogGetPageResponse
            {
                PageCount = request.PageSize > 0 ? logs.Count / request.PageSize: 0,
                RecordCount = logs.Count,
                Items = items.ToList()
            };
            */

            return Task.FromResult(new SmsLogGetPageResponse
            {
                PageCount = 0,
                RecordCount = 0,
                Items = new List<SmsLog>()
            });
        }

        public void Log(SmsLog log)
        {
            var filepath = AppPath.GetFilePath(Config?.Path, Config?.FileName, "sms.log.txt");

            try
            {
                File.AppendAllText(filepath, log.Stringify() + GetLineSeparator() + "\n");
            }
            catch (Exception e)
            {
                logProvider.Error("SmsLoggerTextFile", "write failed", e);
            }
        }

        public Task LogAsync(SmsLog log, CancellationToken cancellation)
        {
            Log(log);

            return Task.CompletedTask;
        }
    }
}
