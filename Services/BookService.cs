using AutoMapper;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using ProgLibrary.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDetailsDto> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);
            return _mapper.Map<BookDetailsDto>(book);
            
        }

        public async Task<BookDetailsDto> GetAsync(string title)
        {
            var book = await _bookRepository.GetAsync(title);
            return _mapper.Map<BookDetailsDto>(book);
        }


        public async Task<IEnumerable<BookDto>> BrowseAsync(string title)
        {
            var books = await _bookRepository.BrowseAsync(title);
            return _mapper.Map<IEnumerable<BookDto>>(books);

        }
       
  
        public async Task<bool> CreateAsync(Guid id, string title, string author, DateTime releasedDate, string description)
        {
            await _bookRepository.GetOrFailAsync(title);
            var book = new Book(id, title, author, releasedDate, description);        
            return await _bookRepository.AddAsync(book);

        }


        public async Task<bool> UpdateAsync(Guid id, string title, string author, DateTime releasedDate, string description)
        {
 
            var book = await _bookRepository.GetOrFailAsync(id);   
            book.SetTitle(title);
            book.SetAuthor(author);
            book.SetDescription(description);
            book.SetReleasedDate(releasedDate);
            return await _bookRepository.UpdateAsync(book);
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var book = await _bookRepository.GetOrFailAsync(id);
            return await _bookRepository.DeleteAsync(book);
       
        }

     
    }
}
