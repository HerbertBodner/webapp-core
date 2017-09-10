using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Dtos;
using WaCore.Crud.Contracts.Services;
using WaCore.Crud.Contracts.Web.ViewModels;

namespace WaCore.Crud.Web.Controllers
{
    public class WacCrudController<TListVm, TDto, TFilter, TEditVm, TNewDto, TEntity, TKey> : WacListDataController<TListVm, TDto, TFilter>
        where TListVm : IWacListDataVm<TDto, TFilter>, new()
        where TFilter : IWacFilter, new()
        where TDto : class
        where TEditVm : IWacEditVm<TDto, TNewDto>, new()
        where TNewDto : class, new()
        where TEntity : class
    {
        protected IWacCrudService<TEntity, TFilter, TDto, TNewDto> CrudService;

        public WacCrudController(IWacCrudService<TEntity, TFilter, TDto, TNewDto> service) :
            base(service)
        {
            CrudService = service;
        }

        

        public async virtual Task<ActionResult> Create()
        {
            var vm = new TEditVm();
            return await PrepareEditVmAsync(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(TEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                return await PrepareEditVmAsync(vm);
            }
            try
            {
                var newDto = await CrudService.CreateAsync(vm.NewDto);
                vm.Dto = newDto;
            }
            catch (Exception ex)
            {
                // ToDo Logging and proper error handling
                ModelState.AddModelError("Error during Create", ex.Message);
                
                return await PrepareEditVmAsync(vm);
            }
            SetCreatedSuccessMessage(vm);

            return RedirectToAction("Index");
        }


        public virtual async Task<ActionResult> Edit(TKey id)
        {
            var vm = new TEditVm();

            var dto = await CrudService.GetAsync(id);
            vm.Dto = dto;
            return await PrepareEditVmAsync(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(TEditVm vm, TKey id)
        {
            if (!ModelState.IsValid)
            {
                return await PrepareEditVmAsync(vm);
            }
            try
            {
                await CrudService.UpdateAsync(id, vm.NewDto);

                SetChangedSuccessMessage(vm);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                // ToDo Logging and proper error handling
                ModelState.AddModelError("Error during Edit", ex.Message);

                return await PrepareEditVmAsync(vm);
            }
        }


        public virtual async Task<IActionResult> Delete(TKey id)
        {
            try
            {
                await CrudService.DeleteAsync(id);

                SetDeletedSuccessMessage(id);

                return new EmptyResult();
            }
            catch (Exception ex)
            {
                // ToDo Logging and proper error handling
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return new EmptyResult();
            }
        }

        protected virtual void SetChangedSuccessMessage(TEditVm vm)
        {
            SetSuccessMessage("Changes saved successful!");
        }

        protected virtual void SetCreatedSuccessMessage(TEditVm vm)
        {
            SetSuccessMessage("Created successful");
        }

        protected virtual void SetDeletedSuccessMessage(object id)
        {
            SetSuccessMessage("Deleted successful");
        }

        public virtual async Task<ActionResult> PrepareEditVmAsync(TEditVm vm)
        {
            return View("Edit", vm);
        }

        protected virtual void SetSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }
    }
}
