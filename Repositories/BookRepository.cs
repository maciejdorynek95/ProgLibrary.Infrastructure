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

        //private static readonly ISet<Book> _books = new HashSet<Book>()
        //{
        //    new Book(Guid.NewGuid(),"Pan Tadeusz","Adam Mickiewicz",DateTime.Parse("31.03.1975"),"Książka o ....1"),
        //    new Book(Guid.NewGuid(),"Lalka","Bolesław Prus",DateTime.Parse("22.06.1977"),"Książka o ....2"),
        //    new Book(Guid.NewGuid(),"Ksiazka 1","Adam 1",DateTime.Parse("31.03.1995"),"Książka o ....3"),
        //    new Book(Guid.NewGuid(),"Ksiazka 2","Adam 2",DateTime.Parse("31.03.1995"),"Książka o ....4"),
        //};

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
            await Task.FromResult(_dbContext.Books.AddAsync(book));
            await Task.FromResult(_dbContext.SaveChangesAsync());
           
        }
        

        public async Task UpdateAsync(Book book)
        {
            _dbContext.Books.Update(book); // zamienic na Upadte z sql
            await _dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Book book)
        => await Task.FromResult(_dbContext.Remove(book));

        
    }
}
