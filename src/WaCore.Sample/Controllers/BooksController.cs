using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WaCore.Sample.Data;
using WaCore.Sample.Entities;

namespace WaCore.Sample.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/books
        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _unitOfWork.BooksRepository.GetAllAsync();
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var book = await _unitOfWork.BooksRepository.GetAsync(id);
            if (book == null)
                return NotFound();
            return Json(book);
        }

        #region UseUoWDocu
        // POST api/books
        [HttpPost]
        public async Task<Book> PostAsync([FromBody]Book book)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                _unitOfWork.BooksRepository.Add(book);
                await _unitOfWork.SaveChangesAsync();
                transaction.Commit();
            }
            return book;
        }
        #endregion

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody]Book book)
        {
            var bookInRepo = _unitOfWork.BooksRepository.GetAsync(id);
            if (bookInRepo == null)
                return NotFound();

            book.Id = id;
            _unitOfWork.BooksRepository.Update(book);
            await _unitOfWork.SaveChangesAsync();
            return Json(book);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var book = await _unitOfWork.BooksRepository.GetAsync(id);
            if (book == null)
                return NotFound();

            _unitOfWork.BooksRepository.Remove(book);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
