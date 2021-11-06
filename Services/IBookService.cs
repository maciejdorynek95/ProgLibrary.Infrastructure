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
        Task<bool> CreateAsync(Guid id, string title, string author, DateTime releasedDate, string description);
        Task<bool> UpdateAsync(Guid id, string title, string author, DateTime releasedDate, string description);
        Task<bool> DeleteAsync(Guid id);

    }
}
