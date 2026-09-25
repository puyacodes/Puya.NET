using System;

namespace Puya.Sms
{
    public class SmsLogGetPageRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool Ascending { get; set; }
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
    }
}
