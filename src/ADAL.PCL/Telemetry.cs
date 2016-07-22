using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
//----------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    public class Telemetry
    {
        private readonly static Telemetry Instance = new Telemetry();

        public static Telemetry GetInstance()
        {
            return Instance;
        }

        DefaultDispatcher Dispatcher = new DefaultDispatcher();

        private IDictionary<List<Tuple<string, string>>,string> EventTracking = new ConcurrentDictionary<List<Tuple<string, string>>,string>();

        string RegisterNewRequest()
        {
            //return the RequestID
        }

        public void RegisterDispatcher(IDispatcher dispatcher, bool aggregationRequired)
        {
            
        }

        internal void StartEvent(string requestId, string eventName)
        {
            
        }

        internal void StopEvent(string requestId, EventsBase Event)
        {
            //remove it from the map
            //time calculation
            //add the response time
        }

        internal void DispatchEventNow(string requestID, EventsBase Event)
        {
            
        }

        internal string StartTime { get; set; }

        internal string EndTime { get; set; }
    }
}
