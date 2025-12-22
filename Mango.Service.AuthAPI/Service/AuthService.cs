using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.DTO;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDBContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Register(RegistrationRequestDTO registrationrequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationrequestDTO.Email,
                Email = registrationrequestDTO.Email,
                NormalizedEmail = registrationrequestDTO.Email.ToUpper(),
                Name = registrationrequestDTO.Name,
                PhoneNumber = registrationrequestDTO.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationrequestDTO.Password);
                if (result.Succeeded) { 
                    var response = _dbContext.ApplicationUsers.First(u =>  u.Email == registrationrequestDTO.Email);

                    UserDTO userDTO = new()
                    {
                        Email = response.Email,
                        Id = response.Id,
                        Name = response.Name,
                        PhoneNumber = response.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch { 
            
            }

            return "Error encountered";
        }
    }
}
