using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Contracts.Data
{
    public interface IWacListDataRepository<TEntity, TFilter>
        where TFilter : IWacFilter
    {
        Tuple<int, IList<TEntity>> GetAll (TFilter filter);
    }
}
