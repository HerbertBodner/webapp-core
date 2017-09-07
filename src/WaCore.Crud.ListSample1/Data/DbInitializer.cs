using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Crud.ListSample1.Entities;

namespace WaCore.Crud.ListSample1.Data
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

            
            dbContext.Add(new Car {Brand = "Audi", Model = "A1", CreatedAt = new DateTime(2001, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "A3", CreatedAt = new DateTime(2003, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "A4", CreatedAt = new DateTime(2004, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "A5", CreatedAt = new DateTime(2005, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "A6", CreatedAt = new DateTime(2006, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "A7", CreatedAt = new DateTime(2007, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "A8", CreatedAt = new DateTime(2008, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "Q2", CreatedAt = new DateTime(2002, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "Q3", CreatedAt = new DateTime(2003, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "Q5", CreatedAt = new DateTime(2005, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "Q7", CreatedAt = new DateTime(2006, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "TT", CreatedAt = new DateTime(2010, 1, 1) });
            dbContext.Add(new Car {Brand = "Audi", Model = "RS/R8", CreatedAt = new DateTime(2008, 1, 1) });
            dbContext.Add(new Car {Brand = "Volkswagen", Model = "Lupo", CreatedAt = new DateTime(1998, 1, 1) });
            dbContext.Add(new Car {Brand = "Volkswagen", Model = "Passat B4", CreatedAt = new DateTime(1993, 1, 1) });
            dbContext.Add(new Car {Brand = "Volkswagen", Model = "Phaeton", CreatedAt = new DateTime(2003, 1, 1) });
            dbContext.Add(new Car {Brand = "Volkswagen", Model = "Polo Mk3", CreatedAt = new DateTime(2001, 1, 1) });
            dbContext.Add(new Car {Brand = "Volkswagen", Model = "Scirocco II", CreatedAt = new DateTime(1992, 1, 1) });
            dbContext.Add(new Car {Brand = "Volkswagen", Model = "SP2", CreatedAt = new DateTime(1976, 1, 1) });
            dbContext.Add(new Car {Brand = "Volkswagen", Model = "Apollo", CreatedAt = new DateTime(1992, 1, 1) });
            dbContext.Add(new Car {Brand = "BMW", Model = "328i Sedan", CreatedAt = new DateTime(2000, 1, 1) });
            dbContext.Add(new Car {Brand = "BMW", Model = "X3 xDrive30i", CreatedAt = new DateTime(2000, 1, 1) });
            dbContext.Add(new Car {Brand = "BMW", Model = "650i Convertible", CreatedAt = new DateTime(2000, 1, 1) });
            dbContext.Add(new Car {Brand = "BMW", Model = "750i Sedan", CreatedAt = new DateTime(2000, 1, 1) });
            dbContext.Add(new Car {Brand = "BMW", Model = "M3 Coupe", CreatedAt = new DateTime(2010, 1, 1) });
            dbContext.SaveChanges();
        }
    }
}
