using WaCore.Web.Middleware.WebApiExceptionHandler;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebApiExceptionHandlerExtensions
    {
        public static void UseWebApiExceptionHandler(this IApplicationBuilder app, WacWebApiExceptionHandlerOptions options = null)
        {
            if (options == null)
            {
                options = new WacWebApiExceptionHandlerOptions();
            }

            app.UseMiddleware<WacWebApiExceptionHandlerMiddleware>(options);
        }
    }
}
