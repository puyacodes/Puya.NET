using Puya.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Puya.Notification
{
    public interface ISmsNotifier
    {
        void Notify(string target, string message);
        void Notify(string target, string templateCode, DynamicModel parameters, object otherData = null);
    }
}
