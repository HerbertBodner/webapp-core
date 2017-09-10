using Microsoft.AspNetCore.Builder;
using WaCore.Web.Middleware.SecureHeaders.Models;

namespace WaCore.Web.Middleware.SecureHeaders
{
    public static class SecureHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseWacSecureHeadersMiddleware(this IApplicationBuilder builder, WacSecureHeadersMiddlewareConfiguration config)
        {
            return builder.UseMiddleware<WacSecureHeadersMiddleware>(config);
        }
    }
}
