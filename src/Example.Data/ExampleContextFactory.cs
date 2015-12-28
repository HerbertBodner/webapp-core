using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using WaCore.Contracts.Data.Repositories.Base;
using Microsoft.Data.Entity;

namespace Example.Data
{
    public class ExampleContextFactory : IDbContextFactory
    {
        public IDbContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WaCoreExample;Trusted_Connection=True;");
            return new ExampleDbContext(optionsBuilder.Options);
        }
    }
}
