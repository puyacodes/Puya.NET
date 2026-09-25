namespace Puya.Sms
{
    public class TextFileSmsProviderConfig: SmsProviderConfig
    {
        public string FileName { get; set; }
        public string Path { get; set; }

        public override string Type { get { return "textfile"; } }
    }
}
