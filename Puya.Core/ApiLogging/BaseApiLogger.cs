using System.Threading;
using System.Threading.Tasks;
using Puya.Extensions;

namespace Puya.ApiLogging
{
    public abstract class BaseApiLogger : IApiLogger
    {
        public BaseApiLogger(ApiClient client, ApiServer server)
        {
            Client = client;
            Server = server;
        }
        public BaseApiLogger()
        { }

        ApiClient client;
        public ApiClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new ApiClient();
                }

                return client;
            }
            set { client = value; }
        }
        ApiServer server;
        public ApiServer Server
        {
            get
            {
                if (server == null)
                {
                    server = new ApiServer();
                }

                return server;
            }
            set { server = value; }
        }

        public abstract void Log(ApiLog log);

        public abstract Task LogAsync(ApiLog log, CancellationToken cancellation);
        protected virtual string Serialize(object obj)
        {
            return obj.SafeSerialize();
        }
    }
}
