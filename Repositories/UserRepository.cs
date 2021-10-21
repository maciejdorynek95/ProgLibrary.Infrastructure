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

        public UserRepository(RoleManager<Role> roleManager, UserManager<User> userManager, LibraryDbContext context)
        {

            _userManager = userManager;
            _context = context;
        }


        public async Task<User> GetAsync(Guid id)
        {
            var user = await Task.FromResult(_userManager.Users.SingleOrDefault(x => x.Id == id));
            if (user == null)
            {
                throw new Exception($"użytkownik o id {id} nie istenieje");
            }
            user.SetRoles(_userManager.GetRolesAsync(user).Result.ToArray());
            return user;
        }

        public async Task<User> GetAsync(string email)
        {
            var user = await Task.FromResult(_userManager.Users.SingleOrDefault(x => x.Email == email));
            if (user != null)
            {
                user.SetRoles(_userManager.GetRolesAsync(user).Result.ToArray());
            }
            return user;

        }

        public async Task<IEnumerable<Reservation>> GetUserReservations(Guid userId)
        {           
            var reservations = await Task.FromResult(new User(_context).Reservations.Where(r=>r.UserId == userId ));        
            return reservations;
        }


        public async Task AddAsync(User user, string password, string role)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, role);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await _userManager.UpdateAsync(user);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            await _userManager.DeleteAsync(user);
            await Task.CompletedTask;
        }



    }
}
