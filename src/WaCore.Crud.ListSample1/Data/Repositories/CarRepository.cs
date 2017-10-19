using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Crud.Contracts.Data;
using WaCore.Crud.Data.Ef;
using WaCore.Crud.ListSample1.Entities;
using WaCore.Crud.ListSample1.ViewModels;

namespace WaCore.Crud.ListSample1.Data.Repositories
{
    public interface ICarRepository : IWacListDataRepository<Car, CarFilter>
    { }

    public class CarRepository : WacListDataRepository<Car, LibraryDbContext, CarFilter>, ICarRepository
    {
        public CarRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Car> ApplyFilter(IQueryable<Car> query, CarFilter filter)
        {
            if (filter.CreatedAfter.HasValue)
            {
                query = query.Where(x => x.CreatedAt > filter.CreatedAfter);
            }

            if (!string.IsNullOrEmpty(filter.Model))
            {
                query = query.Where(x => x.Model.Contains(filter.Model));
            }
            return query;
        }
    }
}
