using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using WaCore.Contracts.Data.Repositories.Base;

namespace WaCore.Data.Repositories.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public readonly IQueryable<TEntity> DbSet;
        protected readonly IDbContext Context;

        public Repository(IDbContextFactory contextFactory)
        {
            Context = contextFactory.GetContext();
            DbSet = Context.Set<TEntity>();
        }


        public virtual void Add(TEntity entity)
        {
            Context.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Context.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Remove(entity);
        }

        public virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
