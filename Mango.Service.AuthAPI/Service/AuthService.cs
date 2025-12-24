using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.DTO;
using Mango.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Mango.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDBContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u  => u.UserName.ToLower() == requestDTO.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user,requestDTO.Password);

            if(user == null && isValid == false)
            {
                return new LoginResponseDTO() { User = null, Token =""};
            }

            UserDTO userDto = new()
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            var token = _jwtTokenGenerator.GenerateToken(user);

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDTO;
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
