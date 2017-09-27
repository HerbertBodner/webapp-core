using System.Collections.Generic;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.Contracts.Utils;
using WaCore.Crud.Utils;

namespace WaCore.Crud.Services
{
    public abstract class WacListDataService<TEntity, TFilter, TDto> : IWacListDataService<TFilter, TDto>
        where TFilter : IWacFilter
    {
        protected readonly IWacListDataRepository<TEntity, TFilter> Repo;

        public WacListDataService(IWacListDataRepository<TEntity, TFilter> repo)
        {
            Repo = repo;
        }

        public async Task<IPagedList<TDto>> GetAllAsync(TFilter filter)
        {
            var entityList = await Repo.GetAllAsync(filter);

            int totalCount;

            // if offset=0 and the amount in the list is smaller than the limit, we already know the total amount and don't need to do another query to the database
            if (filter.Offset == 0 && (filter.Limit == null || entityList.Count <= filter.Limit))
            {
                totalCount = entityList.Count;
            }
            else
            {
                totalCount = await Repo.GetTotalCountAsync(filter);
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
