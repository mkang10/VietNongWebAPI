using Microsoft.AspNetCore.Mvc;
using MilkStoreWepAPI.DTO;
using System.Threading.Tasks;
using VietNongWebAPI.DTO;
using VietNongWebAPI.Repository;
using static VietNongWebAPI.DTO.AuthenticateDTO;

namespace VietNongWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid model state"
                });

            var response = await _userRepository.LoginAsync(dto);

            if (response == null)
            {
                return Unauthorized(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                });
            }

            return Ok(response);
        }

        // Đăng ký
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid model state"
                });

            var response = await _userRepository.RegisterAsync(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // Quên mật khẩu
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDTO
                {
                    IsSuccess = false,
                    Message = "Invalid model state"
                });

            var response = await _userRepository.ForgotPasswordAsync(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
