using Microsoft.AspNetCore.Identity;
using ProgLibrary.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public interface IUserService
    {
        Task<AccountDto> GetAccountAsync(Guid userId);
        Task<AccountDto> GetAccountAsync(string userEmail);
        Task<IEnumerable<AccountDto>> BrowseAsync(string role);
        Task<IdentityResult> RegisterAsync(Guid userId, string email,
            string password, string role = "user");
        Task<TokenDto> LoginAsync(string email, string password);
        Task<IEnumerable<ReservationDto>> GetUserReservations(Guid userId);

        Task<IdentityResult> DeleteAsync(Guid userId);
      
    }
}