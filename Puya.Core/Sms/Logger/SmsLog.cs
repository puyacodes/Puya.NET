using System;

namespace Puya.Sms
{
    public class SmsLog
    {
        public int? Serial { get; set; }
        public int? Id { get; set; }
        public DateTime LogDate { get; set; }
        public string Provider { get; set; }
        public string LineNo { get; set; }
        public string Topic { get; set; }
        public string Category { get; set; }
        public string MobileNo { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
        public string Status { get; set; }
        public string RefCode { get; set; }
        public bool? Success { get; set; }
        public Exception Error { get; set; }
        public object Data { get; set; }
        public SmsLog()
        {
            LogDate = DateTime.Now;
        }
    }
}
