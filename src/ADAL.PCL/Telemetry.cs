﻿//----------------------------------------------------------------------
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
        private readonly static Telemetry Instance = new Telemetry();
        private readonly String format = "yyyy-mm-dd hh:mm:ss.ffffff";

        public static Telemetry GetInstance()
        {
            return Instance;
        }

        DefaultDispatcher Dispatcher = null ;

        private IDictionary<Tuple<string, string>,string> EventTracking = new ConcurrentDictionary<Tuple<string, string>,string>();

        internal string RegisterNewRequest()
        {
            return new Guid().ToString();
        }

        public void RegisterDispatcher(IDispatcher dispatcher, bool aggregationRequired)
        {
            if (aggregationRequired)
            {
                Dispatcher = new DefaultDispatcher(dispatcher);
            }
            else
            {
                Dispatcher = new AggregatedDispatcher(dispatcher);
            }
        }

        internal void StartEvent(string requestId, string eventName)
        {
            if (! EventTracking.ContainsKey(new Tuple<string, string>(requestId, eventName)))
            {
                EventTracking.Add(new Tuple<string, string>(requestId, eventName), DateTime.UtcNow.ToString(format));
            }
        }

        internal void StopEvent(string requestId, EventsBase Event,string eventName)
        {
            string value;
            List<Tuple<string, string>> listEvent = Event.GetEvents();
            if (EventTracking.
                TryGetValue(new Tuple<string, string>(requestId, eventName), out value))
            {
                DateTime startTime = DateTimeOffset.Parse(value).UtcDateTime;
                System.TimeSpan diff1 = DateTime.UtcNow.Subtract(startTime);
                //Add the response time to the list
                listEvent.Add(new Tuple<string, string>(EventConstants.ResponseTime,diff1.ToString(format)));
                //Adding event name to the start of the list
                listEvent.Insert(0, new Tuple<string, string>(EventConstants.EventName,eventName));
                //Remove the event from the tracking Map
                EventTracking.Remove(new Tuple<string, string>(requestId, eventName));
            }
            Dispatcher.Receive(requestId,Event);
        }

        internal void DispatchEventNow(string requestId, EventsBase Event,string eventName)
        {
            EventTracking.Remove(new Tuple<string, string>(requestId, eventName));
            List<Tuple<string, string>> listEvent = Event.GetEvents();
            Dispatcher.Receive(requestId,Event);
        }

        internal int EventsStored()
        {
            return EventTracking.Count;
        }
    }
}
