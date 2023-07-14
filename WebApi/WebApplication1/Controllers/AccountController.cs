using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.User;
using WebApplication1.Helper.Roles;
using WebApplication1.Models;
using WebApplication1.Services.OtpServ;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            AppUser existUser = await _userManager.FindByNameAsync(userRegisterDto.UserName);
            if (existUser != null) return BadRequest();

            string otp = OtpService.GenerateOTP();
            AppUser user = new AppUser();
            user.Email = userRegisterDto.Email;
            user.Name = userRegisterDto.Name;
            user.Surname = userRegisterDto.Surname;
            user.UserName = userRegisterDto.UserName;
            user.ConnectionId = null;
            user.OTP = otp;
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return StatusCode(201);
        }

        [Route("rolegenerator")]
        [HttpGet]
        public async Task<IActionResult> AddRole()
        {

            foreach (var item in Enum.GetValues(typeof(RoleEnum)))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
            }
            return StatusCode(201);
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            if (user == null)
            { 
                user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
                if (user == null)
                {
                    return NotFound();
                }
            }
            //var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            //if (!result.Succeeded)
            //{
            //    return BadRequest(result.Errors);
            //}

        }
    }
}
