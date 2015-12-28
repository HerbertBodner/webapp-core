using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Contracts.Data.Repositories.Base
{
    public interface IDbContext
    {
        int SaveChanges();
        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
    }
}
