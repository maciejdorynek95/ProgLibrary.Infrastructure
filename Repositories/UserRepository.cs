using Microsoft.AspNetCore.Identity;
using ProgLibrary.Core.DAL;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AuthenticationDbContext _dbContext;
        public UserRepository(AuthenticationDbContext dbcontext, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbcontext;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<User> GetAsync(Guid id)
        => await Task.FromResult(_dbContext.Users.SingleOrDefault(x => x.Id == id));
        
        public async Task<User> GetAsync(string email)
           => await Task.FromResult(_dbContext.Users.SingleOrDefault(x => x.Email == email));

        public async Task AddAsync(User user)
        {
            //_dbContext.Users.Add(user);
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
    
            await Task.CompletedTask;
        }     

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        
  
    }
}
