using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Contracts.Web
{
    public interface IWacListDataVm<TDto, TFilter>
        where TDto : class
        where TFilter : IWacFilter
    {
        IList<TDto> DtoList { get; set; }

        TFilter Filter { get; set; }
    }
}
