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
        private static readonly ISet<Reservation> _reservations = new HashSet<Reservation>();
        public async Task<Reservation> GetAsync(Guid id)
         => await Task.FromResult(_reservations.SingleOrDefault(x => x.Id == id));

        public async Task<Reservation> GetASync(User user)
        => await Task.FromResult(_reservations.SingleOrDefault(x => x.Id == user.Id));

        public async Task<IEnumerable<Reservation>> BrowseAsync(Book book)
        {
            var reservations = _reservations.AsEnumerable();
            if (reservations != null)
            {
                reservations = reservations.Where(b => b.Book.Equals(book));
            }
            return await Task.FromResult(reservations);
        }

        public async Task AddASync(Reservation reservation)
         => await Task.FromResult(_reservations.Add(reservation));

        public async Task DeleteAsync(Reservation reservation)
            => await Task.FromResult(_reservations.Remove(reservation));

        public async Task UpdateAsync(Reservation reservation)
         => await Task.CompletedTask;
    }
}
