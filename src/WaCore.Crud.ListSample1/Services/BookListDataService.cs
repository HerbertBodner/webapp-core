﻿using WaCore.Crud.Contracts.Services;
using WaCore.Crud.ListSample1.Data;
using WaCore.Crud.ListSample1.Data.Repositories;
using WaCore.Crud.ListSample1.Dtos;
using WaCore.Crud.ListSample1.Entities;
using WaCore.Crud.ListSample1.ViewModels;
using WaCore.Crud.Services;

namespace WaCore.Crud.ListSample1.Services
{
    public interface IBookListDataService : IWacListDataService<BookFilter, BookDto>
    { }

    public class BookListDataService : WacListDataService<Book, BookFilter, BookDto>, IBookListDataService
    {
        public BookListDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override BookDto MapEntityToDto(Book entity)
        {
            return new BookDto() {
                Id = entity.Id,
                Author = entity.Author,
                Title = entity.Title
            };
        }
    }
}
