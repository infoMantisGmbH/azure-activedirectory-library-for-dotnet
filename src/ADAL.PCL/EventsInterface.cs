using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    internal abstract class EventsInterface
    {
        internal List<Tuple<string,string>> List = new List<Tuple<string,string>>();

        internal static int DefaultEventCount { get; set; }

        internal abstract void SetEvent(string eventName, string eventParameter);

        internal abstract List<Tuple<string, string>> GetEvents(string eventName);

        internal abstract int GetDefaultEventCount();
    }
}
