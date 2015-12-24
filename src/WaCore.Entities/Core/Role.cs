
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WaCore.Entities.Core
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
