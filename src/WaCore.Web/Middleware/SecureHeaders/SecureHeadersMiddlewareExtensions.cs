using WaCore.Web.Middleware.SecureHeaders;
using WaCore.Web.Middleware.SecureHeaders.Models;

namespace Microsoft.AspNetCore.Builder
{
    public static class SecureHeadersMiddlewareExtensions
    {
        public static IApplicationBuilder UseWacSecureHeadersMiddleware(this IApplicationBuilder builder, WacSecureHeadersMiddlewareConfiguration config)
        {
            return builder.UseMiddleware<WacSecureHeadersMiddleware>(config);
        }
    }
}
