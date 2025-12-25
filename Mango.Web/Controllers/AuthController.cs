using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequestDTO = new LoginRequestDTO();
            return View(loginRequestDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            ResponseDTO responseDTO = await _authService.LoginAsync(loginRequestDTO);

            if(responseDTO!= null && responseDTO.IsSuccess)
            {
                LoginResponseDTO loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(responseDTO.Result));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", responseDTO.Message);
                return View(loginRequestDTO);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>() {
                new SelectListItem {Text = StaticDetails.RoleAdmin, Value = StaticDetails.RoleAdmin},
                new SelectListItem {Text = StaticDetails.RoleCustomer, Value = StaticDetails.RoleCustomer},

            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult?> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ResponseDTO responseDTO = await _authService.RegisterAsync(registrationRequestDTO);
            ResponseDTO assignRole;

            if (responseDTO != null && responseDTO.IsSuccess) {
                if (String.IsNullOrEmpty(registrationRequestDTO.RoleName))
                {
                    registrationRequestDTO.RoleName = StaticDetails.RoleCustomer;
                }
                assignRole = await _authService.AssignRoleAsync(registrationRequestDTO);
                if (assignRole != null && assignRole.IsSuccess )
                {
                    TempData["success"] = "Registration successfull";
                    return RedirectToAction(nameof(Login));
                }
            }

            var roleList = new List<SelectListItem>() {
                new SelectListItem {Text = StaticDetails.RoleAdmin, Value = StaticDetails.RoleAdmin},
                new SelectListItem {Text = StaticDetails.RoleCustomer, Value = StaticDetails.RoleCustomer},

            };
            ViewBag.RoleList = roleList;
            return View(registrationRequestDTO);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
