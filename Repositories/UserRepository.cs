using Microsoft.AspNetCore.Identity;
using ProgLibrary.Core.DAL;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {


        private readonly UserManager<User> _userManager;
        private readonly LibraryDbContext _context;

        public UserRepository(UserManager<User> userManager, LibraryDbContext context)
        {

            _userManager = userManager;
            _context = context;
        }


        public async Task<User> GetAsync(Guid id)
        {
            var user = await Task.FromResult( _userManager.Users.FirstOrDefault(x => x.Id == id));
            if (user == null)
            {
                throw new Exception($"użytkownik o id {id} nie istenieje");
            }
            user.GetReservations(_context);
            user.GetRoles(_userManager.GetRolesAsync(user).Result.ToArray());
            return user;
        }

  

        public async Task<User> GetAsync(string email)
        {
            var user = await Task.FromResult( _userManager.Users.Where(x => x.Email == email).FirstOrDefault());
            if (user == null)
            {
                throw new Exception($"użytkownik o email {email} nie istenieje");
            }
            user.GetReservations(_context);
            user.GetRoles(_userManager.GetRolesAsync(user).Result.ToArray());
            return user;

        }

        public async Task<IEnumerable<User>> BrowseAsync(string role = "user")
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            return await Task.FromResult(users);
        }

        public async Task<IEnumerable<Reservation>> GetUserReservations(Guid userId)
        {
            //var reservations = await Task.FromResult(new User(_context).Reservations.Where(r => r.UserId == userId));
            //return reservations;
            var reservations = await Task.FromResult(_context.Reservations.Where(x => x.UserId == userId));
            return reservations;
        }


        public async Task<IdentityResult> AddAsync(User user, string password, string role)
        {
            await _userManager.CreateAsync(user, password);
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);

        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            return await _userManager.DeleteAsync(user);

        }



    }
}
