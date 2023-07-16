using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _config; // we will use this to read appsettings file
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
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
            result = await _userManager.AddToRoleAsync(user, RoleEnum.Admin.ToString());
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


        [Route("login")]
        [HttpPost]
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
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                return NotFound();
            }
            // generate token 
            var userRoles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);  // convering string key to bytes
            var claimList = new List<Claim>(); // claim data will be stored within the claim
            claimList.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));  // instead of ClaimTypes.NameIdentifier we could just write id
            claimList.Add(new Claim("username", user.UserName));
            claimList.Add(new Claim("email", user.Email));
            foreach (var role in userRoles)
            {
                claimList.Add(new Claim("role", role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // all of the description also should be implemented within program class 
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddMinutes(10),
                Issuer = _config["JWT: Issuer"],
                Audience = _config["JWT: Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token), message = "succesfull" });

        }
    }
}
