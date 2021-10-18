using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ProgLibrary.Infrastructure.Services.PasswordHashers
{
    public class BcryptPasswordHasherService : IBcryptPasswordHasherService
    {

        //private static BcryptPasswordHasher settings;
        //public static BcryptPasswordHasher Settings { set { settings = value; } }

 
   
        //private  BcryptPasswordHasher _bcryptPasswordHasher;
        public BcryptPasswordHasherService()
        {
            //IOptions<BcryptPasswordHasher> options
            //_bcryptPasswordHasher.Salt = options.Value.Salt.ToString();
        }
        public string HashPassword(string password)
        {
            string salted = BCrypt.Net.BCrypt.GenerateSalt(12);
            return BCrypt.Net.BCrypt.HashPassword(password, salted);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
