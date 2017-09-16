using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WaCore.Crud.Dtos.Filters;

namespace WaCore.Crud.ListSample1.ViewModels
{
    public class BookFilter : WacFilter
    {
        public string Author { get; set; }

        public BookFilter()
        {
        }

        public override string GetDbSortField(string dtoField)
        {
            return dtoField;
        }
    }
}
