using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Contracts.Data;
using WaCore.Data.Ef;
using WaCore.Data.Ef.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnitOfWorkServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection serviceCollection, Action<RepositoryConfiguration<TDbContext, IWacUnitOfWork>> repositoryConfiguration)
            where TDbContext : DbContext
        {
            return serviceCollection.AddUnitOfWork<TDbContext, IWacUnitOfWork, WacEfUnitOfWork<TDbContext>>(repositoryConfiguration);
        }

        public static IServiceCollection AddUnitOfWork<TDbContext, TUnitOfWorkService, TUnitOfWorkImplementation>(this IServiceCollection serviceCollection, Action<RepositoryConfiguration<TDbContext, TUnitOfWorkService>> repositoryConfiguration, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
            where TDbContext: DbContext
            where TUnitOfWorkService : class, IWacUnitOfWork
            where TUnitOfWorkImplementation : WacEfUnitOfWork<TDbContext>, TUnitOfWorkService
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(TUnitOfWorkService), typeof(TUnitOfWorkImplementation), serviceLifetime));

            var repoConfig = new RepositoryConfiguration<TDbContext, TUnitOfWorkService>(serviceCollection);
            repositoryConfiguration(repoConfig);

            return serviceCollection;
        }
    }
}
