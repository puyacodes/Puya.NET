namespace Puya.Sms
{
    public class JsonFileSmsProviderConfig: TextFileSmsProviderConfig
    {
        public override string Type { get { return "jsonfile"; } }
    }
}
