using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Contracts.Data
{
    public interface IWacListDataRepository<TEntity, TFilter>
        where TFilter : IWacFilter
    {
        Task<IList<TEntity>> GetAllAsync(TFilter filter);

        Task<int> GetTotalCountAsync(TFilter filter);
    }
}
