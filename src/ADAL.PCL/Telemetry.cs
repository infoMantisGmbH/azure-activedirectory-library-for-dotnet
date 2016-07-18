using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    public class Telemetry
    {
        private static Telemetry instance = new Telemetry();

        public static Telemetry GetInstance()
        {
                return instance;
        }

        DefaultDispatcher Dispatcher = new DefaultDispatcher();

        public IDictionary<List<Tuple<string, string>>,string> EventTracking = new ConcurrentDictionary<List<Tuple<string, string>>,string>();

        string RegisterNewRequest()
        {
            //return the RequestID
        }

        void StartEvent(string requestId, string eventName)
        {
            
        }

        void StopEvent(string requestId, EventsInterface Event)
        {
            
        }

        void DispatchEventNow(string requestID, EventsInterface Event)
        {
            
        }
    }
}
