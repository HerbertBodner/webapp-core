using System.Collections.Generic;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Utils;
using WaCore.Crud.Contracts.Web.ViewModels;
using WaCore.Crud.Utils;

namespace WaCore.Crud.Web.ViewModels
{
    public class WacListDataVm<TDto, TFilter> : IWacListDataVm<TDto, TFilter>
        where TFilter : IWacFilter
    {
        public IPagedList<TDto> DtoList { get; set; }

        public TFilter Filter { get; set; }
    }
}
