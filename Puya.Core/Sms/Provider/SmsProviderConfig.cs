using Puya.Collections;

namespace Puya.Sms
{
    public abstract class SmsProviderConfig
    {
        public bool Active { get; set; }
        public abstract string Type { get; }
        public virtual string Name { get; }
        public virtual string EndPoint { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string AccessToken { get; set; }
        public virtual string RefreshToken { get; set; }
        public virtual string LineNo { get; set; }
        public virtual DynamicModel OtherProps { get; set; }
    }
}
