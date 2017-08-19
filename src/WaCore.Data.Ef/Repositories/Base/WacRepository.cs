using System.Linq;
using WaCore.Contracts.Data;
using WaCore.Contracts.Data.Repositories.Base;

namespace WaCore.Data.Repositories.Base
{
    public class WacRepository<TEntity> : IWacRepository<TEntity> where TEntity : class
    {
        public readonly IQueryable<TEntity> DbSet;
        protected readonly WacDbContext Context;

        public WacRepository()
        {
            //TODO Set the context object here somehow
            //Context = 
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
