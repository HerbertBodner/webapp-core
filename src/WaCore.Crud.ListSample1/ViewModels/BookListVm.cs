using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Web;
using WaCore.Crud.Contracts.Web.ViewModels;
using WaCore.Crud.ListSample1.Dtos;
using WaCore.Crud.Web.ViewModels;

namespace WaCore.Crud.ListSample1.ViewModels
{
    public class BookListVm : WacListDataVm<BookDto, BookFilter>, IWacListDataVm<BookDto, BookFilter>
    {
    }
}
