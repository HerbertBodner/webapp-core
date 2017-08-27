using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data;
using WaCore.Data.Ef;
using WaCore.Crud.ListSample1.Data.Repositories;

namespace WaCore.Crud.ListSample1.Data
{
    public class UnitOfWork : WacEfUnitOfWork<LibraryDbContext>, IUnitOfWork
    {
        public UnitOfWork(LibraryDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        { }

        public IBooksListRepository BooksRepository => GetRepository<IBooksListRepository>();
    }

    public interface IUnitOfWork : IWacUnitOfWork
    {
        IBooksListRepository BooksRepository { get; }
    }

}
