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

        public async Task AddAsync(Book book)
        {


            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
           
        }
        

        public async Task UpdateAsync(Book book)
        {
             _dbContext.Books.Update(book); // zamienic na Upadte z sql
            await _dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Book book)
        {
            await Task.FromResult(_dbContext.Remove(book));
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
