using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.ListSample1.Dtos;
using WaCore.Crud.ListSample1.Entities;
using WaCore.Crud.ListSample1.Services;
using WaCore.Crud.ListSample1.ViewModels;
using WaCore.Crud.Web.Controllers;

namespace WaCore.Crud.ListSample1.Controllers
{
    public class CarsController : WacCrudController<CarListVm, CarDto, CarFilter, CarEditVm, NewCarDto, Car, int>
    {
        public CarsController(ICarService service) : base(service)
        {
        }

    }
}
