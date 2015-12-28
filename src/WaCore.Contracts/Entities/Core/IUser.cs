using System;
using System.Collections.Generic;

namespace WaCore.Contracts.Entities.Core
{
    public interface IUser
    {
        string PlainPassword { get; set; }
        string HashedPassword { get; set; }
        string Name { get; set; }


        int AccessFailedCount { get; set; }
        string ConcurrencyStamp { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        Guid Id { get; set; }
        bool LockoutEnabled { get; set; }
        DateTimeOffset? LockoutEnd { get; set; }
        string NormalizedEmail { get; set; }
        string NormalizedUserName { get; set; }
        string PasswordHash { get; set; }
        string PhoneNumber { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        string SecurityStamp { get; set; }
        bool TwoFactorEnabled { get; set; }
        string UserName { get; set; }

        //ICollection<IRole> Roles { get; }
        //ICollection<IdentityUserClaim<TKey>> Claims { get; }

    }
}
