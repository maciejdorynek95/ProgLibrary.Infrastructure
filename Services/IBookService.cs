using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public interface IBookService
    {
        Task<BookDto> GetAsync(Guid id);
        Task<BookDto> GetAsync(string title);
        Task<IEnumerable<BookDto>> BrowseAsync(string title = null);

        Task CreateAsync(Guid id, string title, string author, DateTime deleasedDate, string description);
        Task AddReservationAsync(Guid userId, Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo);
        Task UpdateAsync(Guid id, string title, string author, DateTime deleasedDate, string description);
        Task RemoveAsync(Guid id);

    }
}
