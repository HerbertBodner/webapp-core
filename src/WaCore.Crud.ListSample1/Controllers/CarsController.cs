using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WaCore.Crud.ListSample1.Services;
using WaCore.Crud.ListSample1.ViewModels;

namespace WaCore.Crud.ListSample1.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService _service;
        private CarListVm _listVm;
        public CarsController(ICarService service)
        {
            _service = service;
            _listVm = new CarListVm();
        }


        public async Task<IActionResult> Index(CarFilter filter)
        {
            if (filter == null)
            {
                filter = new CarFilter();
            }
            _listVm.DtoList = await _service.GetListAsync(filter);
            return View(_listVm);
        }

        public async virtual Task<ActionResult> Create()
        {
            var vm = new CarEditVm();
            return await PrepareEditVmAsync(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(CarEditVm vm)
        {
            if (!ModelState.IsValid)
            {
                return await PrepareEditVmAsync(vm);
            }
            try
            {
                var newDto = await _service.CreateAsync(vm.Dto);
                vm.Dto = newDto;
            }
            catch (Exception ex)
            {
                // ToDo Logging and proper error handling
                ModelState.AddModelError("Error during Create", ex.Message);

                return await PrepareEditVmAsync(vm);
            }
            SetSuccessMessage("Created successful"); 

            return RedirectToAction("Index");
        }


        public virtual async Task<ActionResult> Edit(int id)
        {
            var vm = new CarEditVm();

            var dto = await _service.GetAsync(id);
            vm.Dto = dto;
            return await PrepareEditVmAsync(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(CarEditVm vm, int id)
        {
            if (!ModelState.IsValid)
            {
                return await PrepareEditVmAsync(vm);
            }
            try
            {
                await _service.UpdateAsync(id, vm.Dto);

                SetSuccessMessage("Changes saved successful!");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // ToDo Logging and proper error handling
                ModelState.AddModelError("Error during Edit", ex.Message);

                return await PrepareEditVmAsync(vm);
            }
        }


        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                SetSuccessMessage("Deleted successful");

                return new EmptyResult();
            }
            catch (Exception ex)
            {
                // ToDo Logging and proper error handling
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return new EmptyResult();
            }
        }

    
        public virtual async Task<ActionResult> PrepareEditVmAsync(CarEditVm vm)
        {
            return View("Edit", vm);
        }

        protected virtual void SetSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

    }
}
