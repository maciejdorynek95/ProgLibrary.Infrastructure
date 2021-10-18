namespace ProgLibrary.Infrastructure.Services.PasswordHashers
{
    public interface IBcryptPasswordHasherService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}
