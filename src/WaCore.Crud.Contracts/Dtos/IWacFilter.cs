using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace WaCore.Crud.Contracts.Dtos
{
    public interface IWacFilter
    {
        int Offset { get; set; }

        int? Limit { get; set; }

        string SortBy { get; set; }

        string GetDbSortField(string dtoField);
        
    }
}
