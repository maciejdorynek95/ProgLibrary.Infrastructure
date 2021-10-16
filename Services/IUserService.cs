using ProgLibrary.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(Guid userId);
        Task<AccountDto> GetAccountAsync(string userEmail);
        Task RegisterAsync(Guid userId, string email, string name,
            string password, string role = "user");
        Task<TokenDto> LoginAsync(string email, string password);

     

    }
}