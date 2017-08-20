using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Sample.Entities;

namespace WaCore.Sample.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Books.Any())
                return;

            dbContext.Add(new Book { Title = "Crime and Punishment", Author = "Fyodor Dostoyevsky" });
            dbContext.Add(new Book { Title = "The Odyssey", Author = "Homer" });
            dbContext.SaveChanges();
        }
    }
}
