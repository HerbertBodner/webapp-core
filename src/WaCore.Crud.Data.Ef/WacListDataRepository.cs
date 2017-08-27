using System.Collections.Generic;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Dtos.Filters;
using WaCore.Data;
using WaCore.Data.Repositories.Base;

namespace WaCore.Crud.Data.Ef
{
    public abstract class WacListDataRepository<TEntity, TDbContext, TFilter> : WacRepository<TEntity, TDbContext>, IWacListDataRepository<TEntity, TFilter>
        where TEntity : class
        where TDbContext : WacDbContext
        where TFilter : WacFilter
    {
        public WacListDataRepository(TDbContext dbContext) : base(dbContext)
        {
        }

        public abstract List<TEntity> GetAll(TFilter filter);

        //Todo implement generic paging/sorting methods
    }
}
