using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaCore.Contracts.Data;

namespace WaCore.Data.Ef.DependencyInjection
{
    public class RepositoryConfiguration<TDbContext, TUnitOfWorkService>
        where TUnitOfWorkService : IWacUnitOfWork
    {
        private readonly IServiceCollection _serviceCollection;

        public RepositoryConfiguration(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        /// <summary>
        /// Registers a repository class and its implemented interface so it can be created by the UnitOfWork
        /// </summary>
        /// <typeparam name="TService">Interface implemented by the repository</typeparam>
        /// <typeparam name="TImplementation">Repository concrete type. It must have a public constructor which takes the DbContext as argument.</typeparam>
        /// <returns></returns>
        public RepositoryConfiguration<TDbContext, TUnitOfWorkService> AddRepository<TService, TImplementation>()
            where TService: class
            where TImplementation : TService
        {
            // Add repositories not as their implemented interfaces but as factory functions which take dbcontext as argument.
            // This allows the UnitOfWork to create repository instances with dbcontext argument provided at runtime.
            _serviceCollection.AddTransient<Func<TDbContext, TService>>(serviceProvider => dbContext => ActivatorUtilities.CreateInstance<TImplementation>(serviceProvider, dbContext));

            // For convenience register repositories as their interfaces as well, but then use the unit of work to create them
            _serviceCollection.AddTransient<TService>(serviceProvider => serviceProvider.GetRequiredService<TUnitOfWorkService>().GetRepository<TService>());
            return this;
        }

        /// <summary>
        /// Adds repositories from assembly which defines the specified type.
        ///     <para>
        ///      A type is added if the name ends with 'Repisotory' and if it implements an interface with a matching name and the 'I' prefix. E.g. <c>class BooksRepository: IBooksRepository</c>.
        ///     </para>
        /// </summary>
        /// <typeparam name="TAssemblySelector">A type defined in the assembly to scan</typeparam>
        /// <returns></returns>
        public RepositoryConfiguration<TDbContext, TUnitOfWorkService> AddRepositoriesFromAssemblyOf<TAssemblySelector>()
        {
            var assembly = typeof(TAssemblySelector).Assembly;
            var assemblyTypes = assembly.DefinedTypes.Select(ti => ti.AsType());

            var repoDescriptions = assemblyTypes
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
                .Select(t => 
                {
                    var matchingInterface = t.FindInterfaces((it, c) => it.Name == $"I{c}", t.Name).FirstOrDefault();
                    if (matchingInterface == null)
                        return null;
                    return new { Type = t, InterfaceType = matchingInterface };
                })
                .Where(x => x != null);

            var addRepositoryMethod = GetType().GetMethod(nameof(AddRepository));

            foreach (var repoDescription in repoDescriptions)
            {
                var constructedMethod = addRepositoryMethod.MakeGenericMethod(repoDescription.InterfaceType, repoDescription.Type);
                constructedMethod.Invoke(this, new object[]{ });
            }
            return this;
        }
    }
}
