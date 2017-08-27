using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.Contracts.Web;

namespace WaCore.Crud.Web.Controllers
{
    public class WacListDataController<TEntity, TFilter, TDto, TListVm> : Controller
        where TListVm : IWacListDataVm<TDto, TFilter>, new()
        where TFilter : IWacFilter, new()
        where TDto : class
    {
        private readonly IWacListDataService<TEntity, TFilter, TDto> service;
        protected TListVm listVm;
        public WacListDataController(IWacListDataService<TEntity, TFilter, TDto> service)
        {
            this.service = service;
            listVm = new TListVm();
        }

        public IActionResult Index(TFilter filter)
        {
            if (filter == null)
            { 
                filter = new TFilter();
            }
            var dtoList = Filter(filter);
            listVm.DtoList = dtoList;
            return View(listVm);
        }

        protected List<TDto> Filter(TFilter filter)
        {
            return service.GetAll(filter);
        }
    }
}
