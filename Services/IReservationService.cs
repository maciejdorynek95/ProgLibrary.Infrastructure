using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public interface IReservationService
    {
        Task<ReservationDto> GetLastByBookId(Guid bookId);
        Task<IEnumerable<ReservationDto>> GetListByBookId(Guid bookId);     
        Task<IEnumerable<ReservationDto>> BrowseAsync(string bookTitle = null);
        Task<IAsyncResult> CreateAsync(Guid id, Guid userId, Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo );
        Task<IAsyncResult> UpdateAsync(Guid id,  DateTime ReservationTimeFrom, DateTime ReservationTimeTo);
        Task<IAsyncResult> RemoveAsync(Guid id);

    }
}