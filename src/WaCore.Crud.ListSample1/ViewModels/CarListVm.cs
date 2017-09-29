using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Utils;
using WaCore.Crud.ListSample1.Dtos;


namespace WaCore.Crud.ListSample1.ViewModels
{
    public class CarListVm
    {
        public SelectList Models { get; set; }

        public IPagedList<CarDto> DtoList { get; set; }

        public CarFilter Filter { get; set; }
    }
}
