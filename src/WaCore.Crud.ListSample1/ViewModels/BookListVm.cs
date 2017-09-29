using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Utils;
using WaCore.Crud.Contracts.Web;
using WaCore.Crud.Contracts.Web.ViewModels;
using WaCore.Crud.ListSample1.Dtos;

namespace WaCore.Crud.ListSample1.ViewModels
{
    public class BookListVm
    {
        public IPagedList<BookDto> DtoList { get; set; }

        public BookFilter Filter { get; set; }
    }
}
