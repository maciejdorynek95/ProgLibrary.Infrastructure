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


        private readonly AuthenticationDbContext _dbContext;
        public UserRepository(AuthenticationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        //private static readonly ISet<User> _users = new HashSet<User>();

        public async Task<User> GetAsync(Guid id)
        => await Task.FromResult(_dbContext.Users.SingleOrDefault(x => x.Id == id));
        
        public async Task<User> GetAsync(string email)
           => await Task.FromResult(_dbContext.Users.SingleOrDefault(x => x.Email.Trim().ToLowerInvariant() == email.Trim().ToLowerInvariant()));

        public async Task AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
    
            await Task.CompletedTask;
        }

       

        public async Task DeleteAsync(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }
  
    }
}
