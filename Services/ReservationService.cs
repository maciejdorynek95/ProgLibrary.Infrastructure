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
    public class ReservationService : IReservationService
    {
        private IReservationRepository _reservationRepository;
        private IMapper  _mapper;
        public ReservationService(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

  
        public async Task<ReservationDto> GetAsync(Guid bookId)
        {
            var reservation = await _reservationRepository.GetOrFailAsync(bookId);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<IEnumerable<ReservationDto>> GetListAsync(Guid bookId)
        {
            var resevations = await _reservationRepository.GetListOrFailAsync(bookId);
            return _mapper.Map<IEnumerable<ReservationDto>>(resevations);

        }

        public async Task<IEnumerable<ReservationDto>> BrowseAsync(string bookTitle)
        {
            var reservations = await _reservationRepository.BrowseAsync(bookTitle);

            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task CreateAsync(Guid id, Guid userId , Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo)
        {

            var reservation = await _reservationRepository.GetOrFailAsync(id);
            if (reservation != null)
            {
                throw new Exception($"rezerwacja o id: {id} już istnieje");
            }
          
            reservation = new Reservation(id, userId, bookId, ReservationTimeFrom, ReservationTimeTo);
            await _reservationRepository.AddAsync(reservation);
        }  
        public async Task UpdateAsync(Guid id, DateTime ReservationTimeFrom, DateTime ReservationTimeTo)
        {
            var reservation = await _reservationRepository.GetOrFailAsync(id);
            if (reservation == null)
            {
                throw new Exception($"rezerwacja od id {id} nie istnieje");
            }
            await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task RemoveAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetOrFailAsync(id);
            if (reservation == null)
            {
                throw new Exception($"rezerwacja od id {id} nie istnieje");
            }          
            await _reservationRepository.DeleteAsync(reservation);
        }

     
    }
}
