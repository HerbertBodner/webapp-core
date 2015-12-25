
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WaCore.Entities.Core
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
        public string Description { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
