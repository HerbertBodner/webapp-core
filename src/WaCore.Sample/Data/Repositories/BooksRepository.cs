using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data.Repositories.Base;
using WaCore.Data.Repositories.Base;
using WaCore.Sample.Entities;

namespace WaCore.Sample.Data.Repositories
{
    public class BooksRepository : WacRepository<Book, LibraryDbContext>, IBooksRepository
    {
        public BooksRepository(LibraryDbContext dbContext) : base(dbContext)
        { }
    }

    public interface IBooksRepository : IWacRepository<Book>
    {

    }
}