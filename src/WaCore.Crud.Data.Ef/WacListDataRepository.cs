using System.Collections.Generic;
using System.Linq;
using WaCore.Contracts.Data;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Dtos.Filters;
using WaCore.Data;
using WaCore.Data.Repositories.Base;
using WaCore.Crud.Utils;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WaCore.Crud.Data.Ef
{
    public abstract class WacListDataRepository<TEntity, TDbContext, TFilter> : WacRepository<TEntity, TDbContext>, IWacListDataRepository<TEntity, TFilter>
        where TEntity : class
        where TDbContext : WacDbContext
        where TFilter : IWacFilter
    {
        public WacListDataRepository(TDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IList<TEntity>> GetAllAsync(TFilter filter)
        {
            var q = ApplyFilter(DbSet.AsQueryable(), filter);

            var queryPaginated = ApplySortingAndPagination(q, filter);

            return await queryPaginated.ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(TFilter filter)
        {
            var q = ApplyFilter(DbSet.AsQueryable(), filter);

            return await q.CountAsync();
        }


        protected abstract IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, TFilter filter);


        protected IQueryable<TEntity> ApplySortingAndPagination(IQueryable<TEntity> entityList, TFilter filter)
        {
            if ((!string.IsNullOrEmpty(filter.SortField)))
            {
                var dbField = filter.GetDbSortField(filter.SortField);
                if (dbField != null)
                {
                    filter.SortField = dbField;
                }

                if (typeof(TEntity).GetProperty(filter.SortField) != null)
                {
                    entityList = entityList.OrderByField(filter.SortField, filter.SortOrderIsAscending);
                }
            }

            if (filter.Offset > 0)
            {
                entityList = entityList.Skip(filter.Offset);
            }

            if (filter.Limit != null)
            {
                entityList = entityList.Take(filter.Limit.Value);
            }

            return entityList;
        }

    }
}
