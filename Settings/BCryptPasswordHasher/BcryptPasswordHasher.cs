namespace ProgLibrary.Infrastructure.Services.PasswordHashers
{
    public class BcryptPasswordHasher
    {
        public const string Hasher = "Hasher";
        public string Salt { get; set; }
    }

}
