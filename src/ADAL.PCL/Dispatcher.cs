using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    interface IDispatcher
    {
        void Dispatch(List<Tuple<string, string>> Event);
    }
}
