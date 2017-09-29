using System.Threading.Tasks;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Contracts.Services
{
    public interface IWacCrudService<TEntity, TFilter, TDto, TNewDto> : IWacListDataService<TFilter, TDto>
        where TEntity : class, new()
        where TFilter : IWacFilter
    {
        TDto Create(TNewDto dto);
        Task<TDto> CreateAsync(TNewDto dto);

        TDto Update(object id, TNewDto dto);
        Task<TDto> UpdateAsync(object id, TNewDto dto);

        void Delete(object id);
        Task DeleteAsync(object id);

        void MapDtoToEntity(TNewDto dto, TEntity entityToCreateOrUpdate, Operation operation);
    }

    public enum Operation
    {
        Create = 1,
        Update = 2
    }
}