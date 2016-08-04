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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    internal class DefaultEvent : EventsBase
    {
        internal static List<Tuple<string, string>> DefaultEvents = new List<Tuple<string, string>>();

        internal DefaultEvent(string eventName)
        {
            SetEvent(TelemetryStrings.EventName,eventName);
            //Fill in the default parameters
            ApplicationName = PlatformPlugin.PlatformInformation.GetApplicationName();
            SetEvent(TelemetryStrings.ApplicationName,ApplicationName);

            ApplicationVersion = PlatformPlugin.PlatformInformation.GetApplicationVersion();
            SetEvent(TelemetryStrings.ApplicationVersion, ApplicationVersion);

            SdkVersion = AdalIdHelper.GetAdalVersion();
            SetEvent(TelemetryStrings.SdkVersion, SdkVersion);

            //SdkId = AdalIdHelper.GetAssemblyFileVersion();

            DeviceId = PlatformPlugin.PlatformInformation.GetDeviceModel();
            SetEvent(TelemetryStrings.DeviceId, DeviceId);

            Tenant = IdTokenClaim.TenantId;
            SetEvent(TelemetryStrings.Tenant, Tenant);

            Issuer = IdTokenClaim.Issuer;
            SetEvent(TelemetryStrings.Issuer, Issuer);

            Idp = IdTokenClaim.IdentityProvider;
            SetEvent(TelemetryStrings.Idp, Idp);

            Upn = IdTokenClaim.UPN;
            SetEvent(TelemetryStrings.Upn, Upn);

            Email = IdTokenClaim.Email;
            SetEvent(TelemetryStrings.Email, Email);

            PasswordExpiration = IdTokenClaim.PasswordExpiration;
            SetEvent(TelemetryStrings.PasswordExpiration, PasswordExpiration);

            PasswordChangeUrl = IdTokenClaim.PasswordChangeUrl;
            SetEvent(TelemetryStrings.PasswordChangeUrl, PasswordChangeUrl);

            FamilyName = IdTokenClaim.FamilyName;
            SetEvent(TelemetryStrings.FamilyName,FamilyName);
        }

        internal override void SetEvent(string eventName, string eventParameter)
        {
            DefaultEvents.Add(new Tuple<string, string>(eventName, eventParameter));
        }

        internal override List<Tuple<string, string>> GetEvents(string requestId)
        {
            return DefaultEvents;
        }

        internal string eventName { get; set; }

        internal string ClientId { get; set; }

        internal string ClientIp { get; set; }

        internal string ApplicationName { get; set; }

        internal string ApplicationVersion { get; set; }

        internal string SdkId { get; set; }

        internal string SdkVersion { get; set; }

        internal string UserId { get; set; }

        internal string DeviceId { get; set; }

        internal string Tenant { get; set; }

        internal string Issuer { get; set; }

        internal string Idp { get; set; }

        internal string Upn { get; set; }

        internal string Email { get; set; }

        internal string PasswordExpiration { get; set; }

        internal string PasswordChangeUrl { get; set; }

        internal string FamilyName { get; set; }
    }
}