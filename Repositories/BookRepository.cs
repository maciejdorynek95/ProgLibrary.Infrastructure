using ProgLibrary.Core.DAL;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;
        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Book> GetAsync(Guid id)
        => await Task.FromResult(_dbContext.Books.SingleOrDefault(x => x.Id == id));

        public async Task<Book> GetAsync(string title)
          => await Task.FromResult(_dbContext.Books.SingleOrDefault(x => x.Title == title));

        public async Task<IEnumerable<Book>> BrowseAsync(string title = "")
        {
            var books = _dbContext.Books.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(title))
            {
                books = books.Where(x => x.Title.ToLowerInvariant()
                .Contains(title.ToLowerInvariant()));

            }
            return await Task.FromResult(books);
        }

        public async Task<bool> AddAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
        }


        public async Task<bool> UpdateAsync(Book book)
        {
            _dbContext.Books.Update(book);
            return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
        }

        public async Task<bool> DeleteAsync(Book book)
        {
            _dbContext.Remove(book);
            return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
        }


    }
}
