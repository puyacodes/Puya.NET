using Puya.Extensions;
using Puya.Service;
using System;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class SmsSendResponseData
    {
        public object Response { get; set; }
        public string RefCode { get; set; }
        public Exception Error { get; set; }
    }
    public class SmsSendResponse: ServiceResponse<SmsSendResponseData>
    {
        public SmsSendResponse()
        {
            Data = new SmsSendResponseData();
        }
        public static Task<SmsSendResponse> Done()
        {
            var response = new SmsSendResponse();

            response.Succeeded();

            return Task.FromResult(response);
        }
    }
}
