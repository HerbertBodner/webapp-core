﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Data.Ef.DependencyInjection
{
    public class RepositoryConfiguration<TDbContext>
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
        public RepositoryConfiguration<TDbContext> AddRepository<TService, TImplementation>()
            where TImplementation : TService
        {
            // Add repositories not as their implemented interfaces but as factory functions which take dbcontext as argument.
            // This allows the UnitOfWork to create repository instances with dbcontext argument provided at runtime.
            _serviceCollection.AddTransient<Func<TDbContext, TService>>(serviceProvider => dbContext => ActivatorUtilities.CreateInstance<TImplementation>(serviceProvider, dbContext));
            return this;
        }

        // TODO: Create a way to add all repositories from a specific assembly
    }
}