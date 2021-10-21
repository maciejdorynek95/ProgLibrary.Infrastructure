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
        Task<ReservationDto> GetAsyncBookReservation(Guid bookId);
        Task<IEnumerable<ReservationDto>> GetAsyncUserReservations(Guid userId);     
        Task<IEnumerable<ReservationDto>> BrowseAsync(string bookTitle = null);
        Task CreateAsync(Guid id, Guid userId, Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo, DateTime CreatedAt);
        Task UpdateAsync(Guid id,  DateTime ReservationTimeFrom, DateTime ReservationTimeTo);
        Task RemoveAsync(Guid id);

    }
}