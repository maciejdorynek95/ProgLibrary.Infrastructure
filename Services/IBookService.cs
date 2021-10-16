using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public interface IBookService
    {
        Task<BookDetailsDto> GetAsync(Guid id);
        Task<BookDetailsDto> GetAsync(string title);
        Task<IEnumerable<BookDto>> BrowseAsync(string title = null);

        Task CreateAsync(Guid id, string title, string author, DateTime deleasedDate, string description);
        //Task AddReservationAsync(Guid userId, Guid bookId);
        Task UpdateAsync(Guid id, string title, string author, DateTime deleasedDate, string description);
        Task DeleteAsync(Guid id);

    }
}
