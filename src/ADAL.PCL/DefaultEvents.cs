using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    internal class DefaultEvents:EventsInterface
    {
        static internal List<Tuple<string, string>> defaultEvents = new List<Tuple<string, string>>();

        internal void DefaultEvent(string eventName)
        {
            //Fill in the default parameters
        }
        internal override void SetEvent(string eventName, string eventParameter)
        {
            throw new NotImplementedException();
        }

        internal override List<Tuple<string, string>> GetEvents(string eventName)
        {
            throw new NotImplementedException();
        }

        internal override int GetDefaultEventCount()
        {
            throw new NotImplementedException();
        }
    }
}
