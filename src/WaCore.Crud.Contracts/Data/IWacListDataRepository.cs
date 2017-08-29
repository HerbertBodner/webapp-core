using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Contracts.Data
{
    public interface IWacListDataRepository<TEntity, TFilter>
        where TFilter : IWacFilter
    {
        List<TEntity> GetAll(TFilter filter);
    }
}
