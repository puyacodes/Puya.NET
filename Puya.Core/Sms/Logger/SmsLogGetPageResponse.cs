using System.Collections.Generic;

namespace Puya.Sms
{
    public class SmsLogGetPageResponse
    {
        public int PageCount { get; set; }
        public int RecordCount { get; set; }
        public IList<SmsLog> Items { get; set; }
    }
}
