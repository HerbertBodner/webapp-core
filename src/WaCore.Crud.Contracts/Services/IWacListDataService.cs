using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Utils;

namespace WaCore.Crud.Contracts.Services
{
    public interface IWacListDataService<TFilter, TDto>
    {
        TDto Get(object id);
        Task<TDto> GetAsync(object id);
        IPagedList<TDto> GetList(TFilter filter);
        Task<IPagedList<TDto>> GetListAsync(TFilter filter);

    }
}
