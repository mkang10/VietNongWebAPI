using System.ComponentModel.DataAnnotations;

namespace VietNongWebAPI.DTO
{
    public class AuthenticateDTO
    {
        // DTO cho đăng ký người dùng
        public class RegisterDTO
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(6)]
            public string Password { get; set; }
        }

        // DTO cho đăng nhập người dùng
        public class LoginDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        // DTO cho quên mật khẩu
        public class ForgotPasswordDTO
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        // DTO trả về sau khi xác thực
        public class AuthenticationResponseDTO
        {
            public string Token { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
        }
    }
}
