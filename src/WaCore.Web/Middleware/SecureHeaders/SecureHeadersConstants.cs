using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Web.Middleware.SecureHeaders
{
    public static class SecureHeadersConstants
    {
        public static readonly string StrictTransportSecurityHeaderName = "Strict-Transport-Security";

        public static readonly string PublicKeyPinsHeaderName = "Public-Key-Pins";

        public static readonly string XFrameOptionsHeaderName = "X-Frame-Options";

        public static readonly string XssProtectionHeaderName = "X-XSS-Protection";

        public static readonly string ContentSecurityPolicyHeaderName = "Content-Security-Policy";

        public static readonly string ReferrerPolicyHeaderName = "Referrer-Policy";
    }
}
