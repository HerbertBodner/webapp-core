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


        protected override IQueryable<Book> ApplyFilter(IQueryable<Book> query, BookFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Author))
            {
                query = query.Where(x => x.Author.Contains(filter.Author));
            }
            return query;
        }
    }
}