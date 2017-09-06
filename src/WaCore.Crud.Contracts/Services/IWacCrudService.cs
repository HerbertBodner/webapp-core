using System.Threading.Tasks;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Contracts.Services
{
    public interface IWacCrudService<TEntity, TFilter, TDto, TNewDto> : IWacListDataService<TFilter, TDto>
        where TEntity : class
        where TFilter : IWacFilter
    {
        Task<TDto> CreateAsync(TNewDto dto);
        Task<TDto> UpdateAsync(object id, TNewDto dto);
        Task DeleteAsync(object id);
        
        TEntity MapToNewOrUpdatedEntity(Operation operation, TEntity entityToCreateOrUpdate, TNewDto dto);
    }

    public enum Operation
    {
        Create = 1,
        Update = 2
    }
}