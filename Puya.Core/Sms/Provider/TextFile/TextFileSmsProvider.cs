using Puya.Base;
using Puya.Date;
using Puya.Extensions;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Sms
{
    public class TextFileSmsProvider : ISmsProvider
    {
        protected readonly INow now;

        public TextFileSmsProvider(TextFileSmsProviderConfig config, INow now)
        {
            Config = config;
            this.now = now;
        }
        public string Type => Config.Type;

        public TextFileSmsProviderConfig Config { get; }

        protected virtual string GetPath(string defaultFileName)
        {
            return AppPath.GetFilePath(Config.Path, Config.FileName, defaultFileName);
        }
        public virtual Task<SmsSendResponse> SendAsync(SmsSendRequest request, CancellationToken cancellation)
        {
            var path = GetPath("sms.txt");

            try
            {
                File.AppendAllText(path, $@"Date: {now.Value.ToString("yyyy/MM/dd HH:mm:ss.FFFFF")}
Category: {request.Category}
Mobile: {request.Mobile}
Message: {request.Message}
TemplateCode: {request.TemplateCode}
Parameters: {request.Parameters?.Join("\n\t", (kvp, i) => $"{i}. {kvp.Key}: {kvp.Value}")}
Data: {request.OtherData.SafeSerialize()}
{new string('-', 80)}");

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
