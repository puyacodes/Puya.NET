namespace Puya.Sms
{
    public interface ISmsProviderFactory
    {
        ISmsProvider GetProvider(string type);
    }
}