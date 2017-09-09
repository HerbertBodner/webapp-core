using System.Collections.Generic;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.Contracts.Utils;
using WaCore.Crud.Utils;
using WaCore.Crud.Utils.Exceptions;

namespace WaCore.Crud.Services
{
    public abstract class WacListDataService<TEntity, TFilter, TDto> : IWacListDataService<TFilter, TDto>
        where TFilter : IWacFilter
        where TEntity : class
    {
        protected readonly IWacListDataRepository<TEntity, TFilter> repo;

        public WacListDataService(IWacListDataRepository<TEntity, TFilter> repo)
        {
            this.repo = repo;
        }

        public TDto Get(object id)
        {
            var entity = repo.Get(id);
            if (entity == null)
            {
                throw new ResourceNotFoundException(id);
            }
            return MapEntityToDto(entity);
        }

        public async Task<TDto> GetAsync(object id)
        {
            var entity = await repo.GetAsync(id);
            if (entity == null)
            {
                throw new ResourceNotFoundException(id);
            }
            return MapEntityToDto(entity);
        }

        public IPagedList<TDto> GetAll(TFilter filter)
        {
            var entityList = repo.GetAll(filter);

            int totalCount;

            // if offset=0 and the amount in the list is smaller than the limit, we already know the total amount and don't need to do another query to the database
            if (filter.Offset == 0 && (filter.Limit == null || entityList.Count <= filter.Limit))
            {
                totalCount = entityList.Count;
            }
            else
            {
                totalCount = repo.GetTotalCount(filter);
            }

            var dtoList = new List<TDto>();
            foreach (var entity in entityList)
            {
                dtoList.Add(MapEntityToDto(entity));
            }
            return new PagedList<TDto>(dtoList, totalCount, filter.Offset, filter.Limit);
        }

        public async Task<IPagedList<TDto>> GetAllAsync(TFilter filter)
        {
            var entityList = await repo.GetAllAsync(filter);

            int totalCount;

            // if offset=0 and the amount in the list is smaller than the limit, we already know the total amount and don't need to do another query to the database
            if (filter.Offset == 0 && (filter.Limit == null || entityList.Count <= filter.Limit))
            {
                totalCount = entityList.Count;
            }
            else
            {
                totalCount = await repo.GetTotalCountAsync(filter);
            }

            var dtoList = new List<TDto>();
            foreach (var entity in entityList)
            {
                dtoList.Add(MapEntityToDto(entity));
            }
            return new PagedList<TDto>(dtoList, totalCount, filter.Offset, filter.Limit);
        }



        protected abstract TDto MapEntityToDto(TEntity entity);
    }
}
