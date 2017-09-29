﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Crud.ListSample1.Dtos;
using WaCore.Crud.Web.ViewModels;

namespace WaCore.Crud.ListSample1.ViewModels
{
    public class CarListVm : WacListDataVm<CarDto, CarFilter>
    {
        public SelectList Models { get; set; }
    }
}