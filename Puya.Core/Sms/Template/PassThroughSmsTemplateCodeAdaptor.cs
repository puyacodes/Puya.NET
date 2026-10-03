namespace Puya.Sms
{
    public class PassThroughSmsTemplateCodeAdaptor : ISmsTemplateCodeAdaptor<string>
    {
        public string Adapt(string templateCode)
        {
            return templateCode;
        }
    }
}
