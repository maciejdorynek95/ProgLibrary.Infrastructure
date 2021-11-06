using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private IUserRepository _userRepository;
        private IMapper  _mapper;
        public ReservationService(IReservationRepository reservationRepository, IUserRepository userRepositoryIMapper, IMapper mapper )
        {
            _reservationRepository = reservationRepository;
            _userRepository = userRepositoryIMapper;
            _mapper = mapper;
        }

  
        public async Task<ReservationDto> GetLastByBookId(Guid bookId)
        {
            var reservation = await _reservationRepository.GetOrFailAsync(bookId);
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task<IEnumerable<ReservationDto>> BrowseAsync(string bookTitle)
        {
            var reservations = await _reservationRepository.BrowseAsync(bookTitle);
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<IEnumerable<ReservationDto>> GetListByBookId(Guid bookId)
        {
            var resevations = await _reservationRepository.GetListOrFailAsync(bookId);
            return _mapper.Map<IEnumerable<ReservationDto>>(resevations);
        }

        public async Task<IAsyncResult> CreateAsync(Guid id, Guid userId , Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo)
        {
            if (await _userRepository.GetAsync(userId) == null)
            {
                return Task.FromException(new Exception("Użytkownik nie istnieje"));
            }
            
            var reservation = await _reservationRepository.GetAsync(id);
            if (reservation != null)
            {
                return Task.FromException(new Exception("Rezerwacja już istnieje"));
            }   

            reservation = new Reservation(id, userId, bookId, ReservationTimeFrom, ReservationTimeTo);
            return Task.FromResult(await _reservationRepository.AddAsync(reservation));
        }  


        public async Task<IAsyncResult> UpdateAsync(Guid id, DateTime reservationTimeFrom, DateTime reservationTimeTo)
        {
            var reservation = await _reservationRepository.GetOrFailAsync(id);
            reservation = new Reservation(reservation.Id, reservation.UserId, reservation.BookId, reservationTimeFrom, reservationTimeTo);
            return Task.FromResult(await _reservationRepository.UpdateAsync(reservation));
        }


        public async Task<IAsyncResult> RemoveAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetOrFailAsync(id);
            return Task.FromResult(await _reservationRepository.DeleteAsync(reservation));
        }

     
    }
}
