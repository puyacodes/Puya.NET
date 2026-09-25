namespace Puya.Sms
{
    public interface ISmsTemplateCodeAdaptor<TTemplateParams>
    {
        TTemplateParams Adapt(string templateCode);
    }
}
