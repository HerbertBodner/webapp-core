using Microsoft.EntityFrameworkCore;
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
        where TDbContext : DbContext
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
            if ((!string.IsNullOrEmpty(filter.SortBy)))
            {
                var orderList = SortBySplitter.SplitSortByString(filter.SortBy);
                orderList.Reverse();

                foreach (var orderItem in orderList)
                {
                    var sortField = filter.GetDbSortField(orderItem.FieldName);

                    if (sortField != null && typeof(TEntity).GetProperty(sortField) != null)
                    {
                        entityList = entityList.OrderByField(sortField, orderItem.OrderDirection == OrderItem.OrderBy.Ascending);
                    }
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
