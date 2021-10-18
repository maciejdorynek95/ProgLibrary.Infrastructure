using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProgLibrary.Core.Domain;
using ProgLibrary.Core.Repositories;
using ProgLibrary.Infrastructure.DTO;
using ProgLibrary.Infrastructure.Extensions;
using ProgLibrary.Infrastructure.Services.PasswordHashers;
using ProgLibrary.Infrastructure.Settings.JwtToken;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly IBcryptPasswordHasherService _passwordHasher;
        private readonly UserManager<User> _userManager;
        private IHttpContextAccessor _httpContext;

        private IdentityUserRole<Guid> _identityUserRole;

        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper,
            IBcryptPasswordHasherService passwordHasher, IdentityUserRole<Guid> identityUserRole,
            RoleManager<Role> roleManager, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _identityUserRole = identityUserRole;
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContext = httpContextAccessor;
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


        public async Task<TokenDto>? LoginAsync(string email, string password)
        {
    
   
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception("Niepoprawne dane logowania");
            }

            var verification = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            switch (verification)
            {
                case PasswordVerificationResult.Failed:
                    throw new UnauthorizedAccessException("Niepoprawne dane logowania");
                    break;
                case PasswordVerificationResult.Success:


                    //var Base64Pas = System.Convert.ToBase64String(Encoding.ASCII.GetBytes(password));
                    //var Base64Email = System.Convert.ToBase64String(Encoding.ASCII.GetBytes(user.Email));
                    
                    _httpContext.HttpContext.Session.Set("PLU", Encoding.ASCII.GetBytes(user.Email));
                    _httpContext.HttpContext.Session.Set("PLP", Encoding.ASCII.GetBytes(password));

                 
                    //test
                        var userClaims = _httpContext.HttpContext.User.Claims.ToList();
                    userClaims.Add(new Claim("Email",user.Email ));
                    userClaims.Add(new Claim("UserName",user.UserName));
                    //test


                    /// test
                    var name = _httpContext.HttpContext.User.Claims.Where(p => p.Value == user.Email).FirstOrDefault();

                    var assignedRoles = await _userManager.GetRolesAsync(user);
                    var userRoleString = await _roleManager.FindByNameAsync(assignedRoles.FirstOrDefault());
                    // test
                    
                    var jwt = _jwtHandler.CreateToken(user.Id, userRoleString.Name);
                    




                    _httpContext.HttpContext.Session.Set("PLT", Encoding.ASCII.GetBytes(jwt.Token));
                    _httpContext.HttpContext.Session.Set("PLE", Encoding.ASCII.GetBytes(jwt.Expires.ToString()));
                    _httpContext.HttpContext.Session.CommitAsync();

                    return new TokenDto
                    {
                        Token = jwt.Token,
                        Expires = jwt.Expires,
                        Role = userRoleString.Name // zmienic

                    };
                case PasswordVerificationResult.SuccessRehashNeeded:
                    throw new Exception("Wymagane ponowne wygenerowanie hasła");
                default:
                    return new TokenDto
                    {
                        Token = null,
                        Expires = 0,
                        Role = null
                    };
                              
            }
        }


        public async Task RegisterAsync(Guid userId, string email, string name, string password, string role = "user")  // domyślnie jest to  'user'
        {
            var user = await _userRepository.GetAsync(email);
            if (!await _roleManager.RoleExistsAsync(role))
            {
                throw new Exception($"Rola o nazwie: '{role}' nie istnieje");
            }

            //var identityRole  = await _roleManager.FindByNameAsync(role);
            if (user != null)
            {
                throw new Exception($"Użytkownik o mailu: '{email}' już istnieje");
            }
            // password = _passwordHasher.HashPassword(password);
            user = new User(userId, name, email);
            Task.FromResult(await _userManager.CreateAsync(user, password));
            Task.FromResult(await _userManager.AddToRoleAsync(user, role));
            await Task.CompletedTask;




        }

    }

}
