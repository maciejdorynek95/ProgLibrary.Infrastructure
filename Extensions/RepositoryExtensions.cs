using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
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
                throw new Exception($"Książka o tytule: '{title}' już istnieje");
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



    }
}
