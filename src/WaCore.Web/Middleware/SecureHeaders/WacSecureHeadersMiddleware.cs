using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WaCore.Web.Middleware.SecureHeaders.Models;

namespace WaCore.Web.Middleware.SecureHeaders
{
    /// <summary>
    /// A middleware for injecting OWASP recommended headers into a
    /// HTTP Request
    /// </summary>
    public class WacSecureHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly WaSecureHeadersMiddlewareConfiguration _config;

        public WacSecureHeadersMiddleware(RequestDelegate next, WaSecureHeadersMiddlewareConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public void AddHeader(HttpContext httpContext, string headerName, string headerValue)
        {
            if (!httpContext.Response.Headers.ContainsKey(headerName))
            {
                httpContext.Response.Headers.Add(headerName, headerValue);
            }
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (_config == null)
            {
                throw new ArgumentException($@"Expected an instance of the
                        {nameof(WaSecureHeadersMiddlewareConfiguration)} object.");
            }
            if (_config.HstsConfiguration != null)
            {
                AddHeader(httpContext, SecureHeadersConstants.StrictTransportSecurityHeaderName,
                    _config.HstsConfiguration.BuildHeaderValue());
            }

            if (_config.HpkpConfiguration != null)
            {
                AddHeader(httpContext, SecureHeadersConstants.PublicKeyPinsHeaderName,
                    _config.HpkpConfiguration.BuildHeaderValue());
            }

            if (_config.XFrameOptionsConfiguration != null)
            {
                AddHeader(httpContext, SecureHeadersConstants.XFrameOptionsHeaderName,
                    _config.XFrameOptionsConfiguration.BuildHeaderValue());
            }

            if (_config.XssConfiguration != null)
            {
                AddHeader(httpContext, SecureHeadersConstants.XssProtectionHeaderName,
                    _config.XssConfiguration.BuildHeaderValue());
            }
            
            if (_config.ContentSecurityPolicyConfiguration != null)
            {
                AddHeader(httpContext, SecureHeadersConstants.ContentSecurityPolicyHeaderName,
                    _config.ContentSecurityPolicyConfiguration.BuildHeaderValue());
            }

            if (_config.ReferrerPolicy != null)
            {
                AddHeader(httpContext, SecureHeadersConstants.ReferrerPolicyHeaderName,
                    _config.ReferrerPolicy.BuildHeaderValue());
            }

            await _next.Invoke(httpContext);
        }
    }
}
