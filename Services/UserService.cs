using AutoMapper;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using ProgLibrary.Infrastructure.Extensions;
using ProgLibrary.Infrastructure.Services.PasswordHashers;
using ProgLibrary.Infrastructure.Settings.JwtToken;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private IPasswordHasher _passwordHasher;
        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<AccountDto> GetAccountAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId); 
            return _mapper.Map<AccountDto>(user);
        }

        public async Task<AccountDto> GetAccountAsync(string email)
        {
            var user = await _userRepository.GetOrFailAsync(email);
            return _mapper.Map<AccountDto>(user);
        }


        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Niepoprawne dane logowania");
            }
            if (!_passwordHasher.VerifyPassword(password,user.Password))
            {
                throw new UnauthorizedAccessException("Niepoprawne dane logowania");
            }
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }

      
        public async Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user") 
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"Użytkownik o mailu: '{email}' już istnieje");
            }
            password = _passwordHasher.HashPassword(password);
            user = new User(userId, role, name, email, password);
            await _userRepository.AddAsync(user);
        }
    }
    
}
