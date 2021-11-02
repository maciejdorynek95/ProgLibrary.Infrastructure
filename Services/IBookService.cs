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
        Task<int> CreateAsync(Guid id, string title, string author, DateTime releasedDate, string description);
        Task<int> UpdateAsync(Guid id, string title, string author, DateTime releasedDate, string description);
        Task<int> DeleteAsync(Guid id);

    }
}
