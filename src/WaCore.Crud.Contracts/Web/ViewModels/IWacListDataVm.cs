using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Contracts.Web.ViewModels
{
    public interface IWacListDataVm<TDto, TFilter>
        where TFilter : IWacFilter
    {
        IList<TDto> DtoList { get; set; }

        TFilter Filter { get; set; }
    }
}
