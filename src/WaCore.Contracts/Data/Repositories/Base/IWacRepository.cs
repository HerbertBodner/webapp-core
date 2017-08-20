using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WaCore.Contracts.Data.Repositories.Base
{
    public interface IWacRepository<TEntity> where TEntity : class
    {
        TEntity Get(object id);
        Task<TEntity> GetAsync(object id, CancellationToken cancellationToken = default(CancellationToken));
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
