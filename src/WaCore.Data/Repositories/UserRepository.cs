using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Filters;
using WaCore.Contracts.Data.Repositories;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Contracts.Entities.Core;
using WaCore.Data.Repositories.Base;
using WaCore.Entities.Core;

namespace WaCore.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDbContextFactory contextFactory) : base(contextFactory)
        {
        }

        public IQueryable<IUser> GetByUserFilter(UserFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Search))
            {
                return DbSet.Where(x => x.Name.Contains(filter.Search) || x.Email.Contains(filter.Search));
            }
            return DbSet;
        }
    }
}
