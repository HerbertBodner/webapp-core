using WaCore.Web.Middleware.SecureHeaders.Models;

namespace WaCore.Web.Middleware.SecureHeaders
{
    public class WacSecureHeadersMiddlewareConfiguration : ISecureHeadersMiddlewareConfiguration
    {
        /// <summary>
        /// The HTTP Strict Transport Security configuration to use
        /// </summary>
        public HstsConfiguration HstsConfiguration { get; set; }

        /// <summary>
        /// The Public Key Pinning Extension for HTTP configuration to use
        /// </summary>
        public HpkpConfiguration HpkpConfiguration { get; set; }

        /// <summary>
        /// The X-Frame-Options configuration to use
        /// </summary>
        public XFrameOptionsConfiguration XFrameOptionsConfiguration { get; set; }

        /// <summary>
        /// The X-XSS-Protection configuration to use
        /// </summary>
        public XssConfiguration XssConfiguration { get; set; }

        /// <summary>
        /// The Content-Security-Policy configuration to use
        /// </summary>
        public ContentSecurityPolicyConfiguration ContentSecurityPolicyConfiguration { get; set; }

        /// <summary>
        /// The Referrer-Policy configuration to use
        /// </summary>
        public ReferrerPolicyConfiguration ReferrerPolicyConfiguration { get; set; }

        /// <summary>
        /// The X Content Type Options configuration to use
        /// </summary>
        public XContentTypeOptionsConfiguration XContentTypeOptionsConfiguration { get; set; }
    }
}
