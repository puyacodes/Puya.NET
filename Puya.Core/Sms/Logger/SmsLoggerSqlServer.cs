using System;
using System.Threading;
using System.Threading.Tasks;
using Puya.Extensions;
using Puya.Data;
using Puya.Conversion;
using Puya.Date;

namespace Puya.Sms
{
    public class SmsLoggerSqlServer : ISmsLogger
    {
        private readonly IDb db;
        private readonly INow now;

        public SmsLoggerSqlServer(IDb db, INow now)
        {
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
    LogDate,
    Provider,
    LineNo,
    Topic,
    Category,
    MobileNo,
    Message,
    Response,
    Status,
    RefCode,
    Success,
    Error,
    Data
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

            db.ExecuteNonQuerySql(GetInsertQuery(), GetInsertArgs(log));
        }
        public virtual Task LogAsync(SmsLog log, CancellationToken cancellation)
        {
            log.LogDate = now.Value;

            return db.ExecuteNonQuerySqlAsync(GetInsertQuery(), GetInsertArgs(log), cancellation);
        }

        public virtual async Task<SmsLogGetPageResponse> GetPage(SmsLogGetPageRequest request, CancellationToken cancellation)
        {
            var where = $@"
where 1 = 1
    {(string.IsNullOrEmpty(request.Topic) ? "" : "and Topic = @Topic")}
    {(string.IsNullOrEmpty(request.Category) ? "" : "and Category = @Category")}
    {(!request.From.HasValue ? "" : "and LogDate >= @From")}
    {(!request.To.HasValue ? "" : "and LogDate <= @To")}
    {(string.IsNullOrEmpty(request.Provider) ? "" : "and Provider = @Provider")}
    {(string.IsNullOrEmpty(request.LineNo) ? "" : "and LineNo = @LineNo")}
    {(string.IsNullOrEmpty(request.MobileNo) ? "" : "and MobileNo like '%' + @MobileNo + '%'")}
    {(string.IsNullOrEmpty(request.Message) ? "" : "and Message like '%' + @Message + '%'")}
    {(string.IsNullOrEmpty(request.Status) ? "" : "and Status = @Status")}
    {(string.IsNullOrEmpty(request.RefCode) ? "" : "and RefCode = @RefCode")}
    {(!request.Success.HasValue ? "" : "and Success >= @Success")}";

            var recordCount = await db.ExecuteScalarSqlAsync($@"select count(*) from {GetLogTableName()} {where}", request, cancellation);
            var result = new SmsLogGetPageResponse
            {
                RecordCount = SafeClrConvert.ToInt(recordCount),
                Items = await db.ExecuteReaderSqlAsync<SmsLog>($@"
select * from {GetLogTableName()}
{where}
order by LogDate {(request.Ascending ? "asc" : "desc")}
offset @Offset rows fetch next @PageSize rows only", new { Offset = (request.PageIndex - 1) * request.PageSize, request.PageSize }, cancellation)
            };
            
            result.PageCount = (int)Math.Ceiling((double)result.RecordCount / request.PageSize);

            return result;
        }
    }
}
