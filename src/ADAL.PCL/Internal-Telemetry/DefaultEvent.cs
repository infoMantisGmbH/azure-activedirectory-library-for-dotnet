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


namespace Microsoft.IdentityModel.Clients.ActiveDirectory
{
    internal class DefaultEvent : EventsBase
    {
        internal static List<Tuple<string, string>> DefaultEvents = new List<Tuple<string, string>>();

        static DefaultEvent()
        {
            ApplicationName = PlatformPlugin.PlatformInformation.GetApplicationName();

            ApplicationVersion = PlatformPlugin.PlatformInformation.GetApplicationVersion();

            SdkVersion = AdalIdHelper.GetAdalVersion();

            SdkId = AdalIdHelper.GetAssemblyFileVersion();

            DeviceId = PlatformPlugin.CryptographyHelper.CreateSha256Hash(PlatformPlugin.PlatformInformation.GetDeviceModel());
        }

        internal DefaultEvent(string eventName)
        {
            SetEvent(EventConstants.EventName,eventName);
            //Fill in the default parameters

            SetEvent(EventConstants.ApplicationName,ApplicationName);

            SetEvent(EventConstants.ApplicationVersion, ApplicationVersion);

            SetEvent(EventConstants.SdkVersion, SdkVersion);

            SetEvent(EventConstants.SdkId, SdkId);

            SetEvent(EventConstants.DeviceId, DeviceId);

            //TODO :- The idtoken claims will be filled in dynamically
            Tenant = IdTokenClaim.TenantId;
            SetEvent(EventConstants.Tenant, Tenant);

            Issuer = IdTokenClaim.Issuer;
            SetEvent(EventConstants.Issuer, Issuer);

            Idp = IdTokenClaim.IdentityProvider;
            SetEvent(EventConstants.Idp, Idp);

            Upn = IdTokenClaim.UPN;
            SetEvent(EventConstants.Upn, Upn);

            Email = IdTokenClaim.Email;
            SetEvent(EventConstants.Email, Email);

            PasswordExpiration = IdTokenClaim.PasswordExpiration;
            SetEvent(EventConstants.PasswordExpiration, PasswordExpiration);

            PasswordChangeUrl = IdTokenClaim.PasswordChangeUrl;
            SetEvent(EventConstants.PasswordChangeUrl, PasswordChangeUrl);

            FamilyName = IdTokenClaim.FamilyName;
            SetEvent(EventConstants.FamilyName,FamilyName);
        }

        internal override void SetEvent(string eventName, string eventParameter)
        {
            DefaultEvents.Add(new Tuple<string, string>(eventName, eventParameter));
        }

        internal override List<Tuple<string, string>> GetEvents()
        {
            return DefaultEvents;
        }

        internal string eventName { get; set; }

        internal string ClientId { get; set; }

        internal string ClientIp { get; set; }

        internal static string ApplicationName { get; set; }

        internal static string ApplicationVersion { get; set; }

        internal static string SdkId { get; set; }

        internal static string SdkVersion { get; set; }

        internal string UserId { get; set; }

        internal static string DeviceId { get; set; }

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