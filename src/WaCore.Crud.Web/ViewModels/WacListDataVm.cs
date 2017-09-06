using System.Collections.Generic;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Web.ViewModels;

namespace WaCore.Crud.Web.ViewModels
{
    public class WacListDataVm<TDto, TFilter> : IWacListDataVm<TDto, TFilter>
        where TFilter : IWacFilter
    {
        public IList<TDto> DtoList { get; set; }

        public TFilter Filter { get; set; }
    }
}
