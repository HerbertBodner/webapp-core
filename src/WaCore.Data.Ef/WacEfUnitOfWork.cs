using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WaCore.Contracts.Data;

namespace WaCore.Data.Ef
{
    public class WacEfUnitOfWork<TDbContext> : IWacUnitOfWork
        where TDbContext : DbContext, IWacDbContext
    {
        protected readonly TDbContext _dbContext;
        protected readonly IServiceProvider _serviceProvider;
        protected Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public TDbContext DbContext => _dbContext;


        public WacEfUnitOfWork(TDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public async Task<IWacTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WacDbContextTransactionWrapper(await DbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken));
        }

        public async Task<IWacTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new WacDbContextTransactionWrapper(await DbContext.Database.BeginTransactionAsync(cancellationToken));
        }

        public IWacTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return new WacDbContextTransactionWrapper(DbContext.Database.BeginTransaction(isolationLevel));
        }

        public IWacTransaction BeginTransaction()
        {
            return new WacDbContextTransactionWrapper(DbContext.Database.BeginTransaction());
        }


        public void Dispose()
        {
            DbContext.Dispose();
            foreach (var repository in _repositories.Values.Select(r => r as IDisposable).Where(r => r != null))
            {
                repository.Dispose();
            }
        }

        public TRepository GetRepository<TRepository>()
            where TRepository : class
        {
            object repository = null;
            if (!_repositories.TryGetValue(typeof(TRepository), out repository))
            {
                var repositoryFactory = _serviceProvider.GetRequiredService<Func<TDbContext, TRepository>>();
                repository = repositoryFactory(_dbContext);
                _repositories[typeof(TRepository)] = repository;
            }
            return (TRepository)repository;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return DbContext.SaveChangesAsync(cancellationToken);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
