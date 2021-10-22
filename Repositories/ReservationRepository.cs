using ProgLibrary.Core.DAL;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private  readonly LibraryDbContext _context;
        private readonly AuthenticationDbContext _authenticationDb;

        public ReservationRepository(LibraryDbContext context, AuthenticationDbContext authenticationDb)
        {
            _context = context;
            _authenticationDb = authenticationDb;
        }

  
        public async Task<Reservation> GetAsync(Guid reservationId)
         => await Task.FromResult(_context.Reservations.Where(x => x.Id == reservationId).FirstOrDefault());

        public async Task<Reservation> GetAsyncByBook(Guid bookId)
         =>  await Task.FromResult(_context.Reservations.Where(x => x.BookId == bookId).FirstOrDefault());

        public async Task<Reservation> GetAsyncByUser(Guid userId)
         => await Task.FromResult(_context.Reservations.Where(x => x.UserId == userId).FirstOrDefault());

        public async Task<IEnumerable<Reservation>> BrowseAsync(string email = "")
        {

            var reservations = _context.Reservations.Where(r => r.User.Email == email);

      
            if (!string.IsNullOrWhiteSpace(email))
            {
                reservations = reservations.Where(x => x.User.Email == email);
            }
            return await Task.FromResult(reservations);
        }

        public async Task<IEnumerable<Reservation>> GetAsyncListByUser(Guid userId)
        => await Task.FromResult(_context.Reservations.Where(x => x.UserId == userId));

        public async Task<IEnumerable<Reservation>> GetAsyncListByBook(Guid bookId)
        => await Task.FromResult(_context.Reservations.Where(x => x.BookId == bookId));

        public async Task AddAsync(Reservation reservation)
        {
            await Task.FromResult(_context.Reservations.Add(reservation));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await Task.FromResult(_context.SaveChangesAsync());
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await Task.FromResult(_context.SaveChangesAsync());
        }
    }
}
