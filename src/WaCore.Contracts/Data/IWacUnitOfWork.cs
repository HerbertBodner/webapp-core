using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaCore.Contracts.Data
{
    public interface IWacUnitOfWork : IDisposable
    {
        IWacTransaction BeginTransaction(IsolationLevel isolationLevel);
        IWacTransaction BeginTransaction();
        Task<IWacTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<IWacTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default(CancellationToken));
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        TRepository GetRepository<TRepository>()
            where TRepository : class;
    }
}
