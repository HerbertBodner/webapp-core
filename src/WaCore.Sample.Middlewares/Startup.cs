using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WaCore.Common.ExceptionHandling;
using WaCore.Web.Middleware.SecureHeaders;
using WaCore.Web.Middleware.SecureHeaders.Models;
using WaCore.Web.Middleware.WebApiExceptionHandler;

namespace WaCore.Sample.Middlewares
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("secureheaders.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<WacSecureHeadersMiddlewareConfiguration>(Configuration.GetSection("WacSecureHeadersMiddlewareConfiguration"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            IOptions<WacSecureHeadersMiddlewareConfiguration> secureHeaderSettings)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            // secure headers middleware
            app.UseWacSecureHeadersMiddleware(secureHeaderSettings.Value);

            // exception handling middleware
            var webApiExceptionHandlerOptions = new WacWebApiExceptionHandlerOptions
            {
                CatchAll = false,
                HandlingConfiguration = new WacHandlingConfiguration<object, WebApiExceptionDto>()
                    .On<NotImplementedException>(e =>
                        WacHandling.Handled(new WebApiExceptionDto
                        {
                            Message = "Test NoImplementedException response",
                            HttpStatusCode = HttpStatusCode.NotImplemented,
                            ErrorReference = Guid.NewGuid().ToString()
                        }))
            };

            webApiExceptionHandlerOptions.HandlingConfiguration.MatchType = HandlingMatchType.Inheritance;
            app.UseWebApiExceptionHandler(webApiExceptionHandlerOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
