using Puya.Collections;

namespace Puya.Sms
{
    public class SmsSendRequest
    {
        public string Mobile { get; set; }
        public string Message { get; set; }
        public string TemplateCode { get; set; }
        public string Category { get; set; }
        public object OtherData { get; set; }
        public DynamicModel Parameters { get; set; }
    }
}
