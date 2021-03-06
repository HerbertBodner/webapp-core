﻿using System;
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
        private readonly WacSecureHeadersMiddlewareConfiguration _config;

        public WacSecureHeadersMiddleware(RequestDelegate next, WacSecureHeadersMiddlewareConfiguration config)
        {
            _next = next;
            _config = config ?? throw new ArgumentException($@"Expected an instance of the {nameof(WacSecureHeadersMiddlewareConfiguration)} object.");
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

            if (_config.ReferrerPolicyConfiguration != null)
            {
                AddHeader(httpContext, SecureHeadersConstants.ReferrerPolicyHeaderName,
                    _config.ReferrerPolicyConfiguration.BuildHeaderValue());
            }

            if (_config.XContentTypeOptionsConfiguration != null)
            {
                AddHeader(httpContext, SecureHeadersConstants.XContentTypeOptions,
                    _config.XContentTypeOptionsConfiguration.BuildHeaderValue());
            }

            await _next.Invoke(httpContext);
        }
    }
}
