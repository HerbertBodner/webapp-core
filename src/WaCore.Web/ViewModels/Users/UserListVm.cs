using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Filters;
using WaCore.Contracts.Entities.Core;
using WaCore.Entities.Core;

namespace WaCore.Web.ViewModels.Users
{
    public class UserListVm : ListVmBase<User, UserSearchConfig, UserFilter>
    {
        public List<IUser> UserLst { get; set; }

        public UserListVm() : base("User", "Overview of users")
        {
        }
    }
}
