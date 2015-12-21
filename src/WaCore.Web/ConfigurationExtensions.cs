using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Controllers;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using WaCore.Web.Infrastructure;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace WaCore.Web
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureWaCore(this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            services.TryAddSingleton<IRazorViewEngine, WaCoreRazorViewEngine>();
            // WaCoreRazorViewEngine adds /Views/WaCore as the last place to search for views
            // WaCore views are all under Views/WaCore
            // to modify a view just copy it to a higher priority location
            // ie copy /Views/WaCore/Manage/*.cshtml up to /Views/Manage/ and that one will have higher priority
            // and you can modify it however you like
            // upgrading to newer versions of WaCore could modify or add views below /Views/WaCore
            // so you may need to compare your custom views to the originals again after upgrades


            services.AddMvc();
            return services;
        }
    }
}
