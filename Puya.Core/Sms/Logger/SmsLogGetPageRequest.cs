using Puya.Extensions;
using System;

namespace Puya.Sms
{
    public class SmsLogGetPageRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderDir { get; set; }
        public string ThenOrderDir { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Provider { get; set; }
        public string LineNo { get; set; }
        public string Topic { get; set; }
        public string Category { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string RefCode { get; set; }
        public bool? Success { get; set; }
        public string OrderBy { get; set; }
        public string ThenOrderBy { get; set; }
        public void Validate()
        {
            if (Page <= 0)
            {
                Page = 1;
            }
            if (PageSize <= 0)
            {
                PageSize = 15;
            }

            OrderBy = "[" + OrderBy.SelectOf("Serial,Id,LogDate,Provider,LineNo,Topic,Category,MobileNo,Status,RefCode", "LogDate", true, ',') + "]";
            ThenOrderBy = "[" + OrderBy.SelectOf("Serial,Id,LogDate,Provider,LineNo,Topic,Category,MobileNo,Status,RefCode", "", true, ',') + "]";
            OrderDir = OrderDir.SelectOf("asc,desc", "desc", true, ',');
            ThenOrderDir = ThenOrderDir.SelectOf("asc,desc", "asc", true, ',');
        }
    }
}
