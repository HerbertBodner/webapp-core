using Microsoft.EntityFrameworkCore;
using System.Linq;
using WaCore.Contracts.Data.Repositories.Base;

namespace WaCore.Data
{
    public class WaCoreDbContext : IDbContext
    {
        protected DbContext _dbContext;

         #region IDbContext implemetation

        public IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Update(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Remove(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        #endregion
    }
}
