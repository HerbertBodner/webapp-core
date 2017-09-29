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
        where TEntity : class, new()
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
                var entity = new TEntity();
                MapDtoToEntity(dto, entity, Operation.Create);

                Repo.Add(entity);
                UnitOfWork.SaveChanges();
                transaction.Commit();

                return MapEntityToDto(entity);
            }
        }

        public virtual async Task<TDto> CreateAsync(TNewDto dto)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                var entity = new TEntity();
                MapDtoToEntity(dto, entity, Operation.Create);

                Repo.Add(entity);
                await UnitOfWork.SaveChangesAsync();
                transaction.Commit();

                return MapEntityToDto(entity);
            }
        }

        

        public virtual TDto Update(object id, TNewDto dto)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var entity = Repo.Get(id);
                if (entity == null)
                {
                    throw new ResourceNotFoundException(id);
                }

                MapDtoToEntity(dto, entity, Operation.Update);
                Repo.Update(entity);
                UnitOfWork.SaveChanges();
                transaction.Commit();
                return MapEntityToDto(entity);
            }
        }

        public virtual async Task<TDto> UpdateAsync(object id, TNewDto dto)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                var entity = await Repo.GetAsync(id);
                if (entity == null)
                {
                    throw new ResourceNotFoundException(id);
                }

                MapDtoToEntity(dto, entity, Operation.Update);
                Repo.Update(entity);
                await UnitOfWork.SaveChangesAsync();
                transaction.Commit();
                return MapEntityToDto(entity);
            }
        }


        public virtual void Delete(object id)
        {
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var entity = Repo.Get(id);
                if (entity == null)
                {
                    throw new ResourceNotFoundException(id);
                }

                Repo.Remove(entity);
                UnitOfWork.SaveChanges();
                transaction.Commit();
            }
        }

        public virtual async Task DeleteAsync(object id)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                var entity = await Repo.GetAsync(id);
                if (entity == null)
                {
                    throw new ResourceNotFoundException(id);
                }

                Repo.Remove(entity);
                await UnitOfWork.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public abstract void MapDtoToEntity(TNewDto dto, TEntity entityToCreateOrUpdate, Operation operation);
        
    }
}
