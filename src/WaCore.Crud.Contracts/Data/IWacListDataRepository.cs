using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Contracts.Data
{
    public interface IWacListDataRepository<TEntity, TFilter> : IWacRepository<TEntity>
        where TFilter : IWacFilter
        where TEntity : class
    {
        Task<IList<TEntity>> GetAllAsync(TFilter filter);

        Task<int> GetTotalCountAsync(TFilter filter);
    }
}
