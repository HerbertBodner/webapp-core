using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Data;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;
using WaCore.Bl;
using WaCore.Contracts.Data.Repositories;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Data;
using WaCore.Data.Repositories;
using WaCore.Entities.Core;
using WaCore.Web;

namespace Example.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);


            if (env.IsEnvironment("Development"))
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();
            }


            // most common use of environment variables would be in azure hosting
            // since it is added last anything in env vars would trump the same setting in previous config sources
            // so no risk of messing up settings if deploying a new version to azure
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ExampleDbContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.ConfigureWaCoreWeb(Configuration);
            services.ConfigureWaCoreBl(Configuration);

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ExampleDbContext, Guid>()
                .AddDefaultTokenProviders();


            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IDbContextFactory, ExampleContextFactory>();

        }

        public IConfigurationRoot Configuration { get; set; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            if (env.IsEnvironment("Development"))
            {
                //app.UseBrowserLink();

                app.UseDeveloperExceptionPage();

                //app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
                //app.UseStatusCodePagesWithReExecute("/error/{0}");
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }
    }
}
