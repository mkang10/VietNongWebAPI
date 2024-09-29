using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MilkStoreWepAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VietNongWebAPI.DTO;
using VietNongWebAPI.Models;
using VietNongWebAPI.Service;
using static VietNongWebAPI.DTO.AuthenticateDTO;

namespace VietNongWebAPI.Repository
{
    public interface IUserRepository
    {
        Task<AuthenticationResponseDTO> LoginAsync(LoginDTO loginDto);
        Task<ResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<ResponseDTO> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDto);
    }
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<int>> roleManager, IOptionsMonitor<AppSettings> optionsMonitor, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _appSettings = optionsMonitor.CurrentValue;
            _emailService = emailService;
        }

        public async Task<AuthenticationResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !(await _userManager.CheckPasswordAsync(user, dto.Password)))
            {
                return null; // hoặc trả về một đối tượng ResponseDTO với thông báo lỗi
            }

            var token = GenerateToken(user);

            return new AuthenticationResponseDTO
            {
                Token = token,
                Username = user.UserName,
                Email = user.Email,
                Id = user.Id.ToString(),
                PhoneNumber = user.PhoneNumber
            };
        }

        public async Task<ResponseDTO> RegisterAsync(RegisterDTO dto)
        {
            // Kiểm tra role có hợp lệ hay không
            if (dto.Role.ToLower() != "farmer" && dto.Role.ToLower() != "buyer")
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid role. Please specify either 'farmer' or 'buyer'."
                };
            }

            // Tạo đối tượng ApplicationUser
            var user = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.Phonenumber,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };

            // Tạo người dùng mới
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                // Thêm người dùng vào role dựa trên giá trị từ DTO mà không kiểm tra sự tồn tại của role
                await _userManager.AddToRoleAsync(user, dto.Role.ToLower());

                return new ResponseDTO
                {
                    IsSuccess = true,
                    Message = "User registered successfully"
                };
            }
            else
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
        }



        public async Task<ResponseDTO> ForgotPasswordAsync(ForgotPasswordDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var emailSent = await _emailService.SendPasswordResetEmail(user.Email, resetToken);
            if (!emailSent)
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Failed to send reset token"
                };
            }

            return new ResponseDTO
            {
                IsSuccess = true,
                Message = "Password reset token sent to email"
            };
        }

        // Private method to generate JWT token
        private string GenerateToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Id", user.Id.ToString()),
                new Claim("TokenId", Guid.NewGuid().ToString())
            };

            var roles = _userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
