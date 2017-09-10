using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaCore.Contracts.Data;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.Utils.Exceptions;

namespace WaCore.Crud.Services
{
    public abstract class WacCrudService<TEntity, TFilter, TDto, TNewDto> : WacListDataService<TEntity, TFilter, TDto>, IWacCrudService<TEntity, TFilter, TDto, TNewDto> 
        where TFilter : IWacFilter
        where TEntity : class
    {
        protected IWacUnitOfWork UnitOfWork;

        public WacCrudService(IWacUnitOfWork unitOfWork) : base(unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual TDto Create(TNewDto dto)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var entity = MapDtoToNewOrUpdatedEntity(Operation.Create, null, dto);

                repo.Add(entity);
                UnitOfWork.SaveChanges();
                transaction.Commit();

                return MapEntityToDto(entity);
            }
        }

        public virtual async Task<TDto> CreateAsync(TNewDto dto)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                var entity = MapDtoToNewOrUpdatedEntity(Operation.Create, null, dto);

                repo.Add(entity);
                await UnitOfWork.SaveChangesAsync();
                transaction.Commit();

                return MapEntityToDto(entity);
            }
        }

        

        public virtual TDto Update(object id, TNewDto dto)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var entity = repo.Get(id);
                if (entity == null)
                {
                    throw new ResourceNotFoundException(id);
                }

                var updatedEntity = MapDtoToNewOrUpdatedEntity(Operation.Update, entity, dto);
                repo.Update(updatedEntity);
                UnitOfWork.SaveChanges();
                transaction.Commit();
                return MapEntityToDto(updatedEntity);
            }
        }

        public virtual async Task<TDto> UpdateAsync(object id, TNewDto dto)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                var entity = await repo.GetAsync(id);
                if (entity == null)
                {
                    throw new ResourceNotFoundException(id);
                }

                var updatedEntity = MapDtoToNewOrUpdatedEntity(Operation.Update, entity, dto);
                repo.Update(updatedEntity);
                await UnitOfWork.SaveChangesAsync();
                transaction.Commit();
                return MapEntityToDto(updatedEntity);
            }
        }


        public virtual void Delete(object id)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var entity = repo.Get(id);
                if (entity == null)
                {
                    throw new ResourceNotFoundException(id);
                }

                repo.Remove(entity);
                UnitOfWork.SaveChanges();
                transaction.Commit();
            }
        }

        public virtual async Task DeleteAsync(object id)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                var entity = await repo.GetAsync(id);
                if (entity == null)
                {
                    throw new ResourceNotFoundException(id);
                }

                repo.Remove(entity);
                await UnitOfWork.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public abstract TEntity MapDtoToNewOrUpdatedEntity(Operation operation, TEntity entityToCreateOrUpdate, TNewDto dto);
        
    }
}
