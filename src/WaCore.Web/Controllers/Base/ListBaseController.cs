using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using WaCore.Contracts.Bl.Services;
using WaCore.Web.Infrastructure;
using WaCore.Web.ViewModels;

namespace WaCore.Web.Controllers.Base
{
    public abstract class ListBaseController<TListVm, TSearchConfig, TEntity, TFilter> : Controller
        where TListVm : ListVmBase<TEntity, TSearchConfig, TFilter>, new()
        where TSearchConfig : SearchConfig<TFilter>, new()
        where TEntity : class, new()
        where TFilter : class
    {
        public virtual async Task<ActionResult> Index(TListVm vm)
        {
            var viewModel = vm ?? new TListVm();

            return await PrepareListVmAsync(viewModel);
        }

        public virtual async Task<ActionResult> List(TListVm vm)
        {
            if (!ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    //Response.StatusDescription =
                    //    $"Error occured: {string.Join("; ", ModelState.Values.Where(x => x.Errors.Any()).SelectMany(x => x.Errors).Select(x => x.ErrorMessage))}";
                    return new EmptyResult();
                }
            }

            var viewModel = vm ?? new TListVm();

            return await PrepareListVmAsync(viewModel);
        }

        protected virtual async Task<ActionResult> PrepareListVmAsync(TListVm vm)
        {
            try
            {
                await PrepareListVmDataAsync(vm);
            }
            catch (Exception ex)
            {
                if (!Request.IsAjaxRequest())
                    throw;
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                //Response.StatusDescription = $"Error occured: {ex.Message}";
                return new EmptyResult();
            }
            return GetListView(vm);
        }

        protected virtual ActionResult GetListView(TListVm vm)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", vm);
            }
            return View("Index", vm);
        }

        protected abstract Task PrepareListVmDataAsync(TListVm vm);
        //{

        //    var dtoList = await _crudService.GetAllAsync(vm.SearchConfig.ToFilter());

        //    var pageNr = dtoList.Metadata.Offset == 0 ? 1 : (dtoList.Metadata.Offset / dtoList.Metadata.Limit + 1);

        //    var staticPagedList = new StaticPagedList<TDto>(dtoList.DtoList, (pageNr ?? 1),
        //        dtoList.Metadata.Limit ?? dtoList.Metadata.TotalCount, dtoList.Metadata.TotalCount);

        //    vm.DtoPagedList = staticPagedList;
        //}
    }
}
