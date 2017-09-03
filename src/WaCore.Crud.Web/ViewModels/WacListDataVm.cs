using System;
using System.Collections.Generic;
using System.Text;
using WaCore.Crud.Contracts.Web;
using WaCore.Crud.Dtos.Filters;

namespace WaCore.Crud.Web.ViewModels
{
    public class WacListDataVm<TDto, TFilter> : IWacListDataVm<TDto, TFilter>
        where TDto : class
        where TFilter : WacFilter
    {
        public IList<TDto> DtoList { get; set; }

        public TFilter Filter { get; set; }
    }
}
