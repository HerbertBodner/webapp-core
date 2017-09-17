using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Data.Repositories.Base;
using WaCore.Crud.ListSample1.Entities;
using WaCore.Crud.ListSample1.ViewModels;
using WaCore.Crud.Data.Ef;
using WaCore.Crud.Contracts.Data;

namespace WaCore.Crud.ListSample1.Data.Repositories
{
    public interface IBooksListRepository : IWacListDataRepository<Book, BookFilter>
    {

    }


    public class BookListRepository : WacListDataRepository<Book, LibraryDbContext, BookFilter>, IBooksListRepository
    {
        public BookListRepository(LibraryDbContext dbContext) : base(dbContext)
        { }


        public override List<Book> GetAll(BookFilter filter)
        {
            var q = DbContext.Books.Where(x =>
                string.IsNullOrEmpty(filter.Author) || x.Author.Contains(filter.Author));
            return q.ToList();
        }
    }
}