using WaCore.Crud.ListSample1.Services;
using WaCore.Crud.ListSample1.ViewModels;
using WaCore.Crud.ListSample1.Dtos;
using WaCore.Crud.ListSample1.Entities;
using WaCore.Crud.Web.Controllers;

namespace WaCore.Crud.ListSample1.Controllers
{
    public class BookListDataController : WacListDataController<Book, BookFilter, BookDto, BookListVm>
    {
        public BookListDataController(IBookListDataService booksService) : base(booksService)
        {
        }

        
    }
}