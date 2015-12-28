using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Bl.Services;
using WaCore.Contracts.Data.Filters;
using WaCore.Contracts.Data.Repositories;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Entities.Core;
using WaCore.Web.Controllers.Base;
using WaCore.Web.ViewModels.Users;


namespace WaCore.Web.Controllers
{
    public class UserController : ListBaseController<UserListVm, UserSearchConfig, User, UserFilter>
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo) 
        {
            _userRepo = userRepo;
        }

        protected override Task PrepareListVmDataAsync(UserListVm vm)
        {
            var lst = _userRepo.GetByUserFilter(vm.SearchConfig.ToFilter());
            vm.UserLst = lst.ToList();
            return Task.FromResult(0);
        }
    }
}
