using AutoMapper;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<BookDto> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);
            return _mapper.Map<BookDto>(book);
            
        }

        public async Task<BookDto> GetAsync(string title)
        {
            var book = await _bookRepository.GetAsync(title);
            return _mapper.Map<BookDto>(book);
        }


        public async Task<IEnumerable<BookDto>> BrowseAsync(string title = null)
        {
            var books = await _bookRepository.BrowseAsync(title);

            return _mapper.Map<IEnumerable<BookDto>>(books);

        }

        public async Task CreateAsync(Guid id, string title, string authur, DateTime deleasedDate, string description)
        {
            throw new NotImplementedException();
        }

        public async Task AddReservationAsync(Guid userId, Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo)
        {
            throw new NotImplementedException();
        }


        public async Task UpdateAsync(Guid id, string title, string authur, DateTime deleasedDate, string description)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

     
    }
}
