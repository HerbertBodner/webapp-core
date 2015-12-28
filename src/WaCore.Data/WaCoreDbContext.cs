using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Entities.Core;

namespace WaCore.Data
{
    public class WaCoreDbContext : IdentityDbContext<User, Role, Guid>, IDbContext
    {
        public WaCoreDbContext(DbContextOptions options) : base(options)
        {           
        }

        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<UserPermission>().HasKey(x => new {x.PermissionId, x.UserId});
            builder.Entity<RolePermission>().HasKey(x => new { x.PermissionId, x.RoleId });
        }


        #region IDbContext implemetation

        public new IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            base.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            base.Update(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            base.Remove(entity);
        }
        #endregion
    }
}
