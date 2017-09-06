using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Utils;

namespace WaCore.Crud.Contracts.Services
{
    public interface IWacListDataService<TFilter, TDto>
    {
        Task<IPagedList<TDto>> GetAllAsync(TFilter filter);
        Task<TDto> GetAsync(object id);
    }
}
