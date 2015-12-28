using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Filters;

namespace WaCore.Web.ViewModels.Users
{
    public class UserSearchConfig : SearchConfig<UserFilter>
    {
        public string Search { get; set; }

        public bool? OnlyActive { get; set; }
        public override UserFilter ToFilter()
        {
            return new UserFilter
            {
                OnlyActive = OnlyActive,
                Search = Search,
                Limit = Limit,
                Offset = Offset,
                SortField = SortField,
                SortOrderIsAscending = SortOrderIsAscending
            };
        }
    }
}
