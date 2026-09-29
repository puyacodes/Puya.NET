using System;
using System.Threading;
using System.Threading.Tasks;
using Puya.Extensions;
using Puya.Data;
using Puya.Conversion;
using Puya.Date;
using Puya.Service;
using Puya.Reflection;
using System.Linq;

namespace Puya.Sms
{
    public class SmsLoggerSqlServer : ISmsLogger
    {
        private readonly ILogProvider logProvider;
        private readonly IDb db;
        private readonly INow now;

        public SmsLoggerSqlServer(ILogProvider logProvider, IDb db, INow now)
        {
            this.logProvider = logProvider;
            this.db = db;
            this.now = now;
        }
        protected virtual string GetLogTableName()
        {
            return "SmsLog";
        }
        protected virtual string GetInsertQuery()
        {
            return $@"
insert into {GetLogTableName()}
(
    [LogDate],
    [Provider],
    [LineNo],
    [Topic],
    [Category],
    [MobileNo],
    [Message],
    [Response],
    [Status],
    [RefCode],
    [Success],
    [Error],
    [Data]
)
values
(
    @LogDate,
    @Provider,
    @LineNo,
    @Topic,
    @Category,
    @MobileNo,
    @Message,
    @Response,
    @Status,
    @RefCode,
    @Success,
    @Error,
    @Data
)";
        }
        protected virtual object GetInsertArgs(SmsLog log)
        {
            return new
            {
                log.LogDate,
                log.Provider,
                log.LineNo,
                log.Topic,
                log.Category,
                log.MobileNo,
                log.Message,
                Response = log.Response.SafeSerialize(),
                log.Status,
                log.RefCode,
                log.Success,
                Error = log.Error.SafeSerialize(),
                Data = log.Data.SafeSerialize()
            };
        }
        public virtual void Log(SmsLog log)
        {
            log.LogDate = now.Value;

            var query = GetInsertQuery();
            var args = GetInsertArgs(log);

            logProvider.Debug("SmsLoggerSqlServer.Log", query, args);

            db.ExecuteNonQuerySql(query, args);
        }
        public virtual Task LogAsync(SmsLog log, CancellationToken cancellation)
        {
            log.LogDate = now.Value;

            var query = GetInsertQuery();
            var args = GetInsertArgs(log);
            
            logProvider.Debug("SmsLoggerSqlServer.LogAsync", query, args);

            return db.ExecuteNonQuerySqlAsync(query, args, cancellation);
        }

        public virtual async Task<SmsLogGetPageResponse> GetPage(SmsLogGetPageRequest request, CancellationToken cancellation)
        {
            if (request == null)
            {
                return new SmsLogGetPageResponse();
            }

            request.Validate();

            var where = $@"
where 1 = 1
    {(string.IsNullOrEmpty(request.Topic) ? "" : "and [Topic] = @Topic")}
    {(string.IsNullOrEmpty(request.Category) ? "" : "and [Category] = @Category")}
    {(!request.From.HasValue ? "" : "and [LogDate] >= @From")}
    {(!request.To.HasValue ? "" : "and [LogDate] <= @To")}
    {(string.IsNullOrEmpty(request.Provider) ? "" : "and [Provider] = @Provider")}
    {(string.IsNullOrEmpty(request.LineNo) ? "" : "and [LineNo] = @LineNo")}
    {(string.IsNullOrEmpty(request.MobileNo) ? "" : "and [MobileNo] like '%' + @MobileNo + '%'")}
    {(string.IsNullOrEmpty(request.Message) ? "" : "and [Message] like '%' + @Message + '%'")}
    {(string.IsNullOrEmpty(request.Status) ? "" : "and [Status] = @Status")}
    {(string.IsNullOrEmpty(request.RefCode) ? "" : "and [RefCode] = @RefCode")}
    {(!request.Success.HasValue ? "" : "and [Success] = @Success")}";

            var countQuery = $@"select count(*) from {GetLogTableName()} {where}";

            logProvider.Debug("SmsLoggerSqlServer.GetPage", countQuery, request);

            var recordCount = await db.ExecuteScalarSqlAsync(countQuery, request, cancellation);

            var fetchQuery = $@"
select * from {GetLogTableName()}
{where}
order by {request.OrderBy} {request.OrderDir}
{(request.ThenOrderBy == "[]" ? "" : $", {request.ThenOrderBy} {request.ThenOrderDir}")}
offset @Offset rows
fetch next @PageSize rows only";
            var fetchArgs = new { Offset = (request.Page - 1) * request.PageSize, request.PageSize }.Merge(request);
            
            logProvider.Debug("SmsLoggerSqlServer.GetPage", fetchQuery, fetchArgs);

            var items = await db.ExecuteReaderSqlAsync<SmsLog>(fetchQuery, fetchArgs, cancellation);
            var result = new SmsLogGetPageResponse
            {
                RecordCount = SafeClrConvert.ToInt(recordCount),
                Items = items
            };
            
            result.PageCount = (int)Math.Ceiling((double)result.RecordCount / request.PageSize);

            return result;
        }
    }
}
