using Puya.Date;
using Puya.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class JsonFileSmsProvider : TextFileSmsProvider
    {
        public JsonFileSmsProvider(JsonFileSmsProviderConfig config, INow now): base(config, now)
        { }
        public override Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            var path = GetPath("sms.json");
            var content = File.Exists(path) ? File.ReadAllText(path) : "[]";
            var items = content.SafeDeserialize(new List<JsonSmsSendRequest>());

            items.Add(new JsonSmsSendRequest
            {
                Mobile = request.Mobile,
                Message = request.Message,
                TemplateCode = request.TemplateCode,
                Category = request.Category,
                OtherData = request.OtherData,
                Parameters = request.Parameters,
                Date = now.Value
            });

            try
            {
                File.AppendAllText(path, items.SafeSerialize());

                return SmsSendResponse.Done();
            }
            catch (Exception e)
            {
                var response = new SmsSendResponse();

                response.Failed(e);

                return Task.FromResult(response);
            }
        }
    }
}
