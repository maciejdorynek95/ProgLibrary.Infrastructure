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
        private readonly AuthenticationDbContext _authenticationContext;

        public ReservationRepository(LibraryDbContext context, AuthenticationDbContext authenticationContext)
        {
            _context = context;
            _authenticationContext = authenticationContext;
        }

  
        public async Task<Reservation> GetAsync(Guid reservationId)
         => await Task.FromResult(_context.Reservations.Where(x => x.Id == reservationId).FirstOrDefault());

        public async Task<Reservation> GetAsyncByBook(Guid bookId)
         =>  await Task.FromResult(_context.Reservations.Where(x => x.BookId == bookId).FirstOrDefault());

        public async Task<Reservation> GetAsyncByUser(Guid userId)
         => await Task.FromResult(_context.Reservations.Where(x => x.UserId == userId).FirstOrDefault());

        public async Task<IEnumerable<Reservation>> BrowseAsync(string filter = "")
        {

            var reservations = _context.Reservations.AsEnumerable();

      
            if (!string.IsNullOrWhiteSpace(filter))
            {
                reservations = reservations.Where(x => x.User.Email == filter);
            }
            return await Task.FromResult(reservations);
        }
        public async Task<IEnumerable<Reservation>> GetAsyncListOfReservationsByUser(Guid userId)
        {
            if (!_authenticationContext.Users.Where(x => x.Id == userId).Any())
            {
                throw new Exception($"Użytkownik o id {userId} nie istnieje");

            }
            if (!_context.Reservations.Where(x => x.UserId == userId).Any())
            {
                throw new Exception($"Użytkownik o id {userId} nie istnieje");
            }
           
            return await Task.FromResult(_context.Reservations.Where(x => x.UserId == userId));
        }
       

        public async Task<IEnumerable<Reservation>> GetAsyncListOfReservationsByBook(Guid bookId)
        {
            if (!_context.Books.Where(x => x.Id == bookId).Any())
            {
                throw new Exception($"Książka o id {bookId} nie istnieje");

            }

            if (!_context.Reservations.Where(x=>x.BookId == bookId).Any())
            {
                throw new ArgumentNullException($"Brak rezerwacji dla książki o id: {bookId} ");
            }          
            return await Task.FromResult(_context.Reservations.Where(x => x.BookId == bookId));
        }

       

        public async Task<bool> AddAsync(Reservation reservation)
        {
             _context.Reservations.Add(reservation);
            return  Convert.ToBoolean(await _context.SaveChangesAsync());
        }

        public async Task<bool> DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }

        public async Task<bool> UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            return Convert.ToBoolean(_context.SaveChangesAsync());
        }
    }
}
