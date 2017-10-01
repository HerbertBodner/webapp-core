using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Contracts.Data;
using WaCore.Data.Ef;
using WaCore.Sample.Data.Repositories;

namespace WaCore.Sample.Data
{
    #region UnitOfWorkDocu
    public class UnitOfWork : WacEfUnitOfWork<LibraryDbContext>, IUnitOfWork
    {
        public UnitOfWork(LibraryDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        { }

        public IBooksRepository BooksRepository => GetRepository<IBooksRepository>();
    }

    public interface IUnitOfWork : IWacUnitOfWork
    {
        IBooksRepository BooksRepository { get; }
    }
    #endregion
}
