using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Contracts.Data.Filters.Base
{
    public class FilterBase
    {
        public int Offset { get; set; }

        public int? Limit { get; set; }

        public string SortField { get; set; }

        public bool SortOrderIsAscending { get; set; }

        public FilterBase()
        {
            Offset = 0;
            Limit = 50;
            SortOrderIsAscending = true;
        }
    }
}
