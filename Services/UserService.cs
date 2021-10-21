using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using ProgLibrary.Infrastructure.Settings.JwtToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProgLibrary.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private  IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private  RoleManager<Role> _roleManager;
        private  UserManager<User> _userManager;
        private  IHttpContextAccessor _httpContext;



        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper,
                UserManager<User> userManager, RoleManager<Role> roleManager,
                IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;     
            _roleManager = roleManager;
            _httpContext = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<AccountDto> GetAccountAsync(Guid userId)
        {
       
            var user = await _userRepository.GetAsync(userId);          
            return _mapper.Map<AccountDetailsDto>(user);
        }

        public async Task<AccountDto> GetAccountAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
           
            return _mapper.Map<AccountDetailsDto>(user);
        }


        public async Task<TokenDto> LoginAsync(string email, string password)
        {            
            var user = await _userRepository.GetAsync(email.ToLowerInvariant());
            if (user == null)
            {
                throw new Exception("Niepoprawne dane logowania");
            }

            var verification = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            switch (verification)
            {
                case PasswordVerificationResult.Failed:
                    throw new UnauthorizedAccessException("Niepoprawne dane logowania");
              
                case PasswordVerificationResult.Success:             
                    if (_httpContext.HttpContext.Session.IsAvailable)
                    {
                        var assignedRoles = await _userManager.GetRolesAsync(user);
                        var jwt = _jwtHandler.CreateToken(user.Id, assignedRoles);
                        _httpContext.HttpContext.Session.Set("Token", Encoding.ASCII.GetBytes(jwt.Token));
                        await _httpContext.HttpContext.Session.CommitAsync();
                        return new TokenDto
                        {
                            Token = jwt.Token,
                            Expires = jwt.Expires,
                            Role = String.Join(", ", assignedRoles) // zmienic
                        };
                    }
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
                throw new Exception($"Użytkownik o mailu: '{email}' już istnieje");
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
             
                throw new Exception($"Rola o nazwie: '{role}' nie istnieje");
            }          
           
            user = new User(userId, name, email);          
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
