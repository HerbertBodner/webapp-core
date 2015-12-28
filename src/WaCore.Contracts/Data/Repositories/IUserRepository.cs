using System.Linq;
using WaCore.Contracts.Data.Filters;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Contracts.Entities.Core;

namespace WaCore.Contracts.Data.Repositories
{
    public interface IUserRepository<TUser>
        where TUser : IUser
    {
        TUser FindByEmail(string email);
        TUser FindByEmailAndPassword(string email, string hashedPassword);
        TUser Save(TUser user);
    }


    public interface IUserRepository 
    {
        IQueryable<IUser> GetByUserFilter(UserFilter filter);
    }
}
