using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Repositories;
using WaCore.Contracts.Entities.Core;

namespace WaCore.Data.Repositories
{
    public class UserRepository : IUserRepository<IUser>
    {
        public IUser FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IUser FindByEmailAndPassword(string email, string hashedPassword)
        {
            throw new NotImplementedException();
        }

        public IUser Save(IUser user)
        {
            throw new NotImplementedException();
        }
    }
}
