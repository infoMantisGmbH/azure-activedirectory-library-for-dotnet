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
    internal class APIEvent : DefaultEvent
    {
        internal APIEvent() : base(EventConstants.GrantEvent)
        {
            //Fill in default parameters
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
            SetEvent(EventConstants.FamilyName, FamilyName);
        }

        internal override void SetEvent(string eventName, string eventParameter)
        {
            DefaultEvents.Add(new Tuple<string, string>(eventName, eventParameter));
        }

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

