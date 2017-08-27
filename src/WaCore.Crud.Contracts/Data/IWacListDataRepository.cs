using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Crud.Contracts.Data
{
    public interface IWacListDataRepository<TEntity, TFilter>
    {
        List<TEntity> GetAll(TFilter filter);
    }
}
