using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Web.ViewModels;

namespace WaCore.Crud.Web.ViewModels
{
    public class WacEditVm<TDto, TNewDto> : IWacEditVm<TDto, TNewDto>
    {
        public TDto Dto { get; set; }
        public TNewDto NewDto { get; set; }
    }
}
