using System;
using System.Collections.Generic;
using System.Text;

namespace WaCore.Crud.Contracts.Dtos
{
    public interface IWacFilter
    {
        int Offset { get; set; }

        int? Limit { get; set; }

        string SortField { get; set; }

        bool SortOrderIsAscending { get; set; }
    }
}
