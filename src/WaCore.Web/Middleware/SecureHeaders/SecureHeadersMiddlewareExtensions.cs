using Microsoft.AspNetCore.Builder;
using WaCore.Web.Middleware.SecureHeaders.Models;

namespace WaCore.Web.Middleware.SecureHeaders
{
    public static class SecureHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseWaSecureHeadersMiddleware(this IApplicationBuilder builder, WaSecureHeadersMiddlewareConfiguration config)
        {
            return builder.UseMiddleware<WacSecureHeadersMiddleware>(config);
        }
    }
}
