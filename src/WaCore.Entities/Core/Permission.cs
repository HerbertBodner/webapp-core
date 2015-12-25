using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaCore.Entities.Core
{
    public class Permission
    {
        public Permission()
        {
            UserPermissions = new HashSet<UserPermission>();
            RolePermissions = new HashSet<RolePermission>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
