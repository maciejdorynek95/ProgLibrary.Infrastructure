using AutoMapper;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private IReservationRepository _reservationRepository;
        private IMapper  _mapper;
        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public Task<IEnumerable<ReservationDto>> GetAsyncReservations(string bookTitle)
        {
            throw new NotImplementedException();

        }

        public Task<ReservationDto> GetAsyncBookReservation(Guid bookId)
        {
            throw new NotImplementedException();

        }     

        public async Task<IEnumerable<ReservationDto>> GetAsyncUserReservations(Guid id)
        {
            var resevations = await _reservationRepository.GetAsyncReservations(id);
            return _mapper.Map<IEnumerable<ReservationDto>>(resevations);

        }

       
        public async Task<IEnumerable<ReservationDto>> BrowseAsync(string bookTitle)
        {
            var reservations = await _reservationRepository.BrowseAsync(bookTitle);

            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task CreateAsync(Guid id, Guid userId, Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo, DateTime ReservationTime)
        {
            var reservation = await _reservationRepository.GetAsyncReservation(id);
            reservation = new Reservation(id, userId, bookId, ReservationTimeFrom, ReservationTimeTo);
            await _reservationRepository.AddAsync(reservation);
        }
    
   
        public Task UpdateAsync(Guid id, DateTime ReservationTimeFrom, DateTime ReservationTimeTo)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

     
    }
}
