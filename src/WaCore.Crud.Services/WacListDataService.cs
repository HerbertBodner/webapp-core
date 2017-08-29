using System.Collections.Generic;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Services;

namespace WaCore.Crud.Services
{
    public abstract class WacListDataService<TEntity, TFilter, TDto> : IWacListDataService<TFilter, TDto>
        where TFilter : IWacFilter
    {
        private readonly IWacListDataRepository<TEntity, TFilter> repo;

        public WacListDataService(IWacListDataRepository<TEntity, TFilter> repo)
        {
            this.repo = repo;
        }

        public List<TDto> GetAll(TFilter filter)
        {
            var entityList = repo.GetAll(filter);

            return entityList.ConvertAll(MapEntityToDto);
        }

        protected abstract TDto MapEntityToDto(TEntity entity);
    }
}
