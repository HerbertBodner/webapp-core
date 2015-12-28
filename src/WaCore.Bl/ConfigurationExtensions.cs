using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;
using WaCore.Bl.Services.Account;
using WaCore.Contracts.Bl.Services.Account;

namespace WaCore.Bl
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureWaCoreBl(this IServiceCollection services,
            IConfigurationRoot configuration)
        {
            
            services.TryAddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}
