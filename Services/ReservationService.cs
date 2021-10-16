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

        public async Task<ReservationDto> GetAsync(Guid id)
        {
            var resevation = await _reservationRepository.GetAsync(id);
            return _mapper.Map<ReservationDto>(resevation);

        }

        public Task<ReservationDto> GetAsyncBookId(Guid bookId)
        {
            throw new NotImplementedException();

        }
        public async Task<IEnumerable<ReservationDto>> BrowseAsync(Book book)
        {
            var reservations = await _reservationRepository.BrowseAsync(book);

            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public Task CreateAsync(Guid id, Guid userId, Guid bookId, DateTime ReservationTimeFrom, DateTime ReservationTimeTo, DateTime ReservationDate)
        {
            throw new NotImplementedException();
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
