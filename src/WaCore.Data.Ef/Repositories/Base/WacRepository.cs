using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WaCore.Contracts.Data;
using WaCore.Contracts.Data.Repositories.Base;

namespace WaCore.Data.Repositories.Base
{
    public class WacRepository<TEntity, TDbContext> : IWacRepository<TEntity> 
        where TEntity : class
        where TDbContext : WacDbContext
    {
        protected readonly DbSet<TEntity> DbSet;
        protected readonly TDbContext DbContext;

        public WacRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public virtual TEntity Get(object id)
        {
            return DbSet.Find(id);
        }

        public virtual Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return DbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        public virtual IList<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public virtual void Add(TEntity entity)
        {
            DbContext.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbContext.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DbContext.Remove(entity);
        }
    }
}
