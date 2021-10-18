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
        private readonly LibraryDbContext _context;

        public ReservationRepository(LibraryDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Reservation>> BrowseAsync(string bookTitle = "")
        {
            var reservations = _context.Reservations.AsEnumerable();
            var book = _context.Books.Where(b => b.Title == bookTitle).FirstOrDefault();
            
            if (!string.IsNullOrWhiteSpace(bookTitle))
            {
                reservations = reservations.Where(x => x.BookId == book.Id);
              

            }
            return await Task.FromResult(reservations);
        }

        public async Task<Reservation> GetAsyncReservation(Guid bookId)
         => await Task.FromResult(_context.Reservations.Where(x => x.BookId == bookId).FirstOrDefault());

        public async Task<List<Reservation>> GetAsyncReservations(Guid userId)
        => await Task.FromResult(_context.Reservations.Where(x => x.UserId == userId).ToList());



        public async Task AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            await Task.FromResult(_context.Reservations.Remove(reservation));
            await Task.FromResult(_context.SaveChangesAsync());
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            await Task.FromResult(_context.Reservations.Update(reservation));
            await Task.FromResult(_context.SaveChangesAsync());
        }
    }
}
