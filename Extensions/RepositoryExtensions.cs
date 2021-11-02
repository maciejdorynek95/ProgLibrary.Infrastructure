using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using ProgLibrary.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<Book> GetOrFailAsync(this IBookRepository bookRepository, Guid id)
        {
            var book = await bookRepository.GetAsync(id);
            if (book == null)
            {
                throw new Exception($"Książka o nazwie: '{book}' nie istnieje");
            }
            return book;


        }

        public static async Task<Book> GetOrFailAsync(this IBookRepository bookRepository, string title)
        {
            var book = await bookRepository.GetAsync(title);
            if (book != null)
            {
                throw new BookException($"Książka o tytule: '{title}' już istnieje");
            }
            return book;


        }

        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, Guid id)
        {
            var user = await userRepository.GetAsync(id);
            if (user == null)
            {
                throw new Exception($"Użytkownik o id: '{id}' nie istnieje");
            }
            return user;


        }

        public static async Task<User> GetOrFailAsync(this IUserRepository userRepository, string email)
        {
            var user = await userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception($"Użytkownik o e-mailu: '{email}' nie istnieje");
            }
            return user;


        }

        public static async Task<Reservation> GetOrFailAsync(this IReservationRepository reservationRepository, Guid id)
        {
            var reservation = await reservationRepository.GetAsync(id);
            if (reservation == null)
            {
                throw new Exception($"Rezerwacja o id: '{id}' nie istenieje");
            }
            return reservation;
        }

        public static async Task<IEnumerable<Reservation>> GetListOrFailAsync(this IReservationRepository reservationRepository, Guid id)
        {
            var reservations = await reservationRepository.GetAsyncListOfReservationsByBook(id);
            if (reservations == null)
            {
                throw new Exception($"Książka o id: '{id}' nie istnieje");
            }
            return reservations;
        }


    }
}
