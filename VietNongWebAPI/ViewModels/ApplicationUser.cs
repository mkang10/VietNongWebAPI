using Microsoft.AspNetCore.Identity;

namespace VietNongWebAPI.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
