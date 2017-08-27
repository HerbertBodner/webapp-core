using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Crud.Contracts.Services
{
    public interface IWacListDataService<TEntity, TFilter, TDto>
    {
        List<TDto> GetAll(TFilter filter);
    }
}
