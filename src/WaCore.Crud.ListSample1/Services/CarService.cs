using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.ListSample1.Data;
using WaCore.Crud.ListSample1.Data.Repositories;
using WaCore.Crud.ListSample1.Dtos;
using WaCore.Crud.ListSample1.Entities;
using WaCore.Crud.ListSample1.ViewModels;
using WaCore.Crud.Services;

namespace WaCore.Crud.ListSample1.Services
{
    public interface ICarService : IWacCrudService<Car, CarFilter, CarDto, NewCarDto>
    { }

    public class CarService : WacCrudService<Car, CarFilter, CarDto, NewCarDto>, ICarService
    {
        public CarService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override void MapDtoToEntity(NewCarDto dto, Car entityToCreateOrUpdate, Operation operation)
        {
            entityToCreateOrUpdate.Model = dto.Model;
            entityToCreateOrUpdate.Brand = dto.Brand;
        }

        protected override CarDto MapEntityToDto(Car entity)
        {
            return new CarDto {
                Id = entity.Id,
                Brand = entity.Brand,
                Model = entity.Model,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
