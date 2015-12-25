using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using WaCore.Contracts.Entities.Core;

namespace WaCore.Entities.Core
{
    public class User : IdentityUser<Guid>, IUser
    {
        public User()
        {
            UserPermissions = new HashSet<UserPermission>();
        }

        public string PlainPassword { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
