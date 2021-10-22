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
        Task<ReservationDto> GetAsync(Guid bookId);
        Task<IEnumerable<ReservationDto>> GetListAsync(Guid bookId);     
        Task<IEnumerable<ReservationDto>> BrowseAsync(string bookTitle = null);
        Task CreateAsync(Guid id, Guid userId, Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo );
        Task UpdateAsync(Guid id,  DateTime ReservationTimeFrom, DateTime ReservationTimeTo);
        Task RemoveAsync(Guid id);

    }
}