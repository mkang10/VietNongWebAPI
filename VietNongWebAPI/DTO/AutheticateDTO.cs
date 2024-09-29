using System.ComponentModel.DataAnnotations;

namespace VietNongWebAPI.DTO
{
    public class AuthenticateDTO
    {
        // DTO cho đăng ký người dùng
        public class RegisterDTO
        {
            [Required(ErrorMessage = "Username is required.")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Phone number is required.")]
            public string Phonenumber { get; set; } // Thêm thuộc tính Phonenumber

            [Required(ErrorMessage = "Address is required.")]
            public string Address { get; set; } // Thêm thuộc tính Address

            public string Role { get; set; }
        }

        // DTO cho đăng nhập người dùng
        public class LoginDTO
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            public string Password { get; set; }
        }

        // DTO cho quên mật khẩu
        public class ForgotPasswordDTO
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string Email { get; set; }
        }

        // DTO trả về sau khi xác thực
        public class AuthenticationResponseDTO
        {
            public string Token { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Id { get; set; } // Thêm Id nếu cần
            public string PhoneNumber { get; set; } // Thêm PhoneNumber nếu cần
            // Thêm các thuộc tính khác nếu cần
        }
    }
}
