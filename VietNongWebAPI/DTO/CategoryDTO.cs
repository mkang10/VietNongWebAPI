namespace VietNongWebAPI.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }

    public class CategoryCreateDTO
    {
        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }

    public class CategoryUpdateDTO
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
