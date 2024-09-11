using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MilkStoreWepAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VietNongWebAPI.DTO;
using VietNongWebAPI.ViewModels;
using static VietNongWebAPI.DTO.AuthenticateDTO;

namespace VietNongWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appSettings;

        public UsersController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid model state"
                });

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, dto.Password)))
            {
                return Unauthorized(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                });
            }

            var token = GenerateToken(user);

            return Ok(new ResponseDTO
            {
                IsSuccess = true,
                Message = "Authentication successful",
                Result = token
            });
        }

        private string GenerateToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Id", user.Id),
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var user = new ApplicationUser { UserName = dto.Username, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                return Ok(new { result = (object)null, isSuccess = true, message = "User registered successfully" });
            }
            else
            {
                return BadRequest(new { result = (object)null, isSuccess = false, message = string.Join(", ", result.Errors.Select(e => e.Description)) });
            }
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return BadRequest(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "User not found"
                });
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Gửi email chứa token để người dùng đặt lại mật khẩu

            return Ok(new ResponseDTO
            {
                IsSuccess = true,
                Message = "Password reset token sent to email"
            });
        }
    }
}
