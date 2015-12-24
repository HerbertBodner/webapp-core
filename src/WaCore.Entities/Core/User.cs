using System;
using Microsoft.AspNet.Identity.EntityFramework;
using WaCore.Contracts.Entities.Core;

namespace WaCore.Entities.Core
{
    public class User : IdentityUser<Guid>, IUser
    {
        public string PlainPassword { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }
    }
}
