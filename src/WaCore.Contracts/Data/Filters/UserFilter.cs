using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Filters.Base;

namespace WaCore.Contracts.Data.Filters
{
    public class UserFilter : FilterBase
    {
        public bool? OnlyActive { get; set; }

        public string Search { get; set; }
    }
}
