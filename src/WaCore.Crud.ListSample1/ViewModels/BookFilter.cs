using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Crud.Dtos.Filters;

namespace WaCore.Crud.ListSample1.ViewModels
{
    public class BookFilter : WacFilter
    {
        public string Author { get; set; }
    }
}
