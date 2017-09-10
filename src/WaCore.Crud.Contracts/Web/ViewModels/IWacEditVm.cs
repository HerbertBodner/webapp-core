using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Crud.Contracts.Web.ViewModels
{
    public interface IWacEditVm<TDto, TNewDto>
    {
        TDto Dto { get; set; }
        TNewDto NewDto { get; set; }
    }
}
