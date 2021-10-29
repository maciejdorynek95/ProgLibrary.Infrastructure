using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using ProgLibrary.Infrastructure.Settings.JwtToken;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProgLibrary.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<UserService> _logger;



        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper,
                UserManager<User> userManager, RoleManager<Role> roleManager,
                IHttpContextAccessor httpContextAccessor, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _roleManager = roleManager;
            _httpContext = httpContextAccessor;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<AccountDto> GetAccountAsync(Guid userId)
        {

            var user = await _userRepository.GetAsync(userId);
            _logger.LogInformation($"GetAccountAsync{userId} : ", user);
            return _mapper.Map<AccountDetailsDto>(user);
        }

        public async Task<AccountDto> GetAccountAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            _logger.LogInformation($"GetAccountAsync{email} : ", user);
            return _mapper.Map<AccountDetailsDto>(user);
        }


        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            if (email == null)
            {
                _logger.LogInformation("Nie podano adresu e-mail");
                throw new UnauthorizedAccessException("Nie podano adresu e-mail");

            }

            var user = await _userRepository.GetAsync(email.ToLowerInvariant());
            if (user == null)
            {
                _logger.LogInformation("Niepoprawne dane logowania");
                throw new UnauthorizedAccessException("Niepoprawne dane logowania");
            }

            var verification = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            switch (verification)
            {
                case PasswordVerificationResult.Failed:
                    _logger.LogInformation("Niepoprawne dane logowania");
                    throw new UnauthorizedAccessException("Niepoprawne dane logowania");

                case PasswordVerificationResult.Success:
                    _logger.LogInformation("Dane Logowania Poprawne", email, password);
                    if (_httpContext.HttpContext.Session.IsAvailable)
                    {
                        var assignedRoles = await _userManager.GetRolesAsync(user);
                        var jwt = _jwtHandler.CreateToken(user, assignedRoles);
                        return new TokenDto
                        {
                            Token = jwt.Token,
                            Expires = jwt.Expires,
                            Role = String.Join(", ", assignedRoles) // zmienic
                        };
                    }
                    _logger.LogInformation("Sesja jest niedostępna");
                    throw new Exception("Sesja jest niedostępna");

                case PasswordVerificationResult.SuccessRehashNeeded:
                    throw new Exception("Wymagane ponowne wygenerowanie hasła");
                default:
                    return new TokenDto
                    {
                        Token = "Błąd generowania tokenu",
                        Expires = 0,
                        Role = null
                    };
            }
        }


        public async Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user")  // domyślnie jest to  'user'
        {


            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                _logger.LogInformation($"Użytkownik o mailu: '{email}' już istnieje");
                throw new Exception($"Użytkownik o mailu: '{email}' już istnieje");
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                _logger.LogInformation($"Rola o nazwie: '{role}' nie istnieje");
                throw new Exception($"Rola o nazwie: '{role}' nie istnieje");
            }

            user = new User(userId, name, email);
            _logger.LogInformation($"Utworzono User : '{role}' ");
            await _userRepository.AddAsync(user, password, role);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<ReservationDto>> GetUserReservations(Guid userId)
        {
            var userBooks = await _userRepository.GetUserReservations(userId);
            return _mapper.Map<IEnumerable<ReservationDto>>(userBooks);
        }
    }
}
