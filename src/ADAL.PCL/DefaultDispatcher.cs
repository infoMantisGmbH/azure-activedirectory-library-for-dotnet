using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    internal class DefaultDispatcher
    {
        internal IDictionary<string, List<Tuple<string, string>>> ObjectsToBeDispatched = new ConcurrentDictionary<string, List<Tuple<string, string>>>();

        internal IDispatcher Dispatch { get; set; }

        internal DefaultDispatcher()
        {
            
        }

        internal DefaultDispatcher(IDispatcher logger)
        {
            
        }

        internal virtual void Flush()
        {
            
        }

        internal virtual void Receive(string requestId, EventsInterface eventsInterface)
        {
            
        }

    }
}
