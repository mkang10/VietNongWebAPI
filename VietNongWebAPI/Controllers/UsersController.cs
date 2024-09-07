using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MilkStoreWepAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VietNongWebAPI.DTO;
using VietNongWebAPI.Models;

namespace VietNongWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly VietNongContext _context;
        private readonly AppSettings _appSettings;

        public UsersController(VietNongContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        [HttpPost("Login")]

        public IActionResult Validate(LoginDTO dto)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == dto.Email && u.Password == dto.Password);
            if (user == null)// User không tồn tại
            {
                return Ok(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid email/password"
                });
            }
            //cấp token
            return Ok(new ResponseDTO
            {
                IsSuccess = true,
                Message = "Authentication success",
                Result = GenerateToken(user)

            });
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescripton = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Id", user.UserId.ToString()),

                    //roles

                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescripton);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}

