using WaCore.Crud.ListSample1.Services;
using WaCore.Crud.ListSample1.ViewModels;
using WaCore.Crud.ListSample1.Dtos;
using WaCore.Crud.ListSample1.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WaCore.Crud.ListSample1.Controllers
{
    public class BookListDataController : Controller 
    {
        private readonly IBookListDataService _service;
        protected BookListVm _listVm;

        public BookListDataController(IBookListDataService booksService)
        {
            _service = booksService;
            _listVm = new BookListVm();
        }

        public async Task<IActionResult> Index(BookFilter filter)
        {
            if (filter == null)
            {
                filter = new BookFilter();
            }
            _listVm.DtoList = await _service.GetAllAsync(filter);
            return View(_listVm);
        }

    }
}