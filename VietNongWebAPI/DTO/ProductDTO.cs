using VietNongWebAPI.Models;

namespace VietNongWebAPI.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public int? UserId { get; set; }

        public string? Name { get; set; }

        public int? CategoryId { get; set; }

        public decimal? Price { get; set; }

        public decimal? Weight { get; set; }

        public string? Description { get; set; }

        public int? StockQuantity { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
    public class ProductCreateDTO
    {
        public int? UserId { get; set; }
        public string? Name { get; set; }

        public int? CategoryId { get; set; }

        public decimal? Price { get; set; }

        public decimal? Weight { get; set; }

        public string? Description { get; set; }

        public int? StockQuantity { get; set; }
    }

    public class ProductUpdateDTO
    {
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        public string? Name { get; set; }

        public int? CategoryId { get; set; }

        public decimal? Price { get; set; }

        public decimal? Weight { get; set; }

        public string? Description { get; set; }

        public int? StockQuantity { get; set; }

    }
}
