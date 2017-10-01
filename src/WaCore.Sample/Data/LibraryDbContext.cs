using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Data;
using WaCore.Sample.Entities;

namespace WaCore.Sample.Data
{
    #region LibraryDbcontextDocu
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        { }

        public DbSet<Book> Books { get; set; }
    }
    #endregion
}
