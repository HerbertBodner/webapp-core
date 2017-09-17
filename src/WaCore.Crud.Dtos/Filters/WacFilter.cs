using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WaCore.Crud.Contracts.Dtos;

namespace WaCore.Crud.Dtos.Filters
{
    public class WacFilter : IWacFilter
    {
        public int Offset { get; set; }

        public int? Limit { get; set; }

        public string SortBy { get; set; }

        public WacFilter()
        {
            Offset = 0;
        }
    }
}
