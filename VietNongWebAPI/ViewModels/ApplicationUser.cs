using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace VietNongWebAPI.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        // Các thuộc tính bổ sung từ bảng User hiện tại
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        // Các quan hệ khác
        public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Revenue> Revenues { get; set; } = new List<Revenue>();
    }
}
