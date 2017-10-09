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
using WaCore.Crud.Utils.Sorting;
using System;
using System.Threading.Tasks;

namespace WaCore.Crud.Data.Ef
{
    public abstract class WacListDataRepository<TEntity, TDbContext, TFilter> : WacRepository<TEntity, TDbContext>, IWacListDataRepository<TEntity, TFilter>
        where TEntity : class
        where TDbContext : DbContext
        where TFilter : IWacFilter
    {
        protected virtual SortFieldMapping<TEntity> SortFieldMapping { get; set; }

        public WacListDataRepository(TDbContext dbContext) : base(dbContext)
        {
        }

        public IList<TEntity> GetList(TFilter filter)
        {
            var q = ApplyFilter(DbSet.AsQueryable(), filter);

            var queryPaginated = ApplySortingAndPagination(q, filter);

            return queryPaginated.ToList();
        }
        
        protected void InitializeSortFieldMapping(Action<ISortFieldMappingBuilder<TEntity>> configAction)
        {
            var builder = new SortFieldMappingBuilder<TEntity>();
            configAction(builder);
            SortFieldMapping = builder.Build();
        }

        public async Task<IList<TEntity>> GetListAsync(TFilter filter)
        {
            var q = ApplyFilter(DbSet.AsQueryable(), filter);

            var queryPaginated = ApplySortingAndPagination(q, filter);

            return await queryPaginated.ToListAsync();
        }


        public int GetTotalCount(TFilter filter)
        {
            var q = ApplyFilter(DbSet.AsQueryable(), filter);

            return q.Count();
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
                entityList = entityList.OrderBySortString(filter.SortBy, SortFieldMapping);
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
