using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.Contracts.Utils;
using WaCore.Crud.Contracts.Web.ViewModels;

namespace WaCore.Crud.Web.Controllers
{
    public class WacListDataController<TListVm, TDto, TFilter> : Controller
        where TListVm : IWacListDataVm<TDto, TFilter>, new()
        where TFilter : IWacFilter, new()
        where TDto : class
    {
        private readonly IWacListDataService<TFilter, TDto> service;
        protected TListVm listVm;
        public WacListDataController(IWacListDataService<TFilter, TDto> service)
        {
            this.service = service;
            listVm = new TListVm();
        }

        public async Task<IActionResult> Index(TFilter filter)
        {
            if (filter == null)
            { 
                filter = new TFilter();
            }
            listVm.DtoList = await FilterAsync(filter);
            return View(listVm);
        }

        protected async Task<IPagedList<TDto>> FilterAsync(TFilter filter)
        {
            return await service.GetAllAsync(filter);
        }
    }
}
