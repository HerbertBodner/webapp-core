namespace WaCore.Contracts.Data.Repositories.Base
{
    public interface IRepository<in TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        int SaveChanges();
    }
}
