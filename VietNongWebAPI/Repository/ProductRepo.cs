using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VietNongWebAPI.DTO;
using VietNongWebAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace VietNongWebAPI.Repository
{
    public interface IProductRepo
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<int> CreateAsync(ProductCreateDTO productCreateDTO);
        Task<int> UpdateAsync(ProductUpdateDTO productUpdateDTO);
        Task<bool> RemoveAsync(int id);
    }

    public class ProductRepo : IProductRepo
    {
        private readonly VietNongContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;  // Sử dụng UserManager của Identity

        public ProductRepo(VietNongContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;  // Inject UserManager
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? null : _mapper.Map<ProductDTO>(product);
        }

        public async Task<int> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            // Kiểm tra xem CategoryId có tồn tại không
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == productCreateDTO.CategoryId);
            if (!categoryExists)
            {
                throw new ArgumentException("Category does not exist.");
            }

            // Kiểm tra xem UserId có tồn tại không (sử dụng UserManager từ Identity)
            var userExists = await _userManager.FindByIdAsync(productCreateDTO.UserId.ToString());
            if (userExists == null)
            {
                throw new ArgumentException("User does not exist.");
            }

            // Ánh xạ DTO sang entity
            var product = _mapper.Map<Product>(productCreateDTO);
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            // Kiểm tra xem ProductId có tồn tại không
            var existingProduct = await _context.Products.FindAsync(productUpdateDTO.ProductId);
            if (existingProduct == null)
            {
                throw new ArgumentException("Product does not exist.");
            }

            // Kiểm tra xem CategoryId có tồn tại không
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == productUpdateDTO.CategoryId);
            if (!categoryExists)
            {
                throw new ArgumentException("Category does not exist.");
            }

            // Kiểm tra xem UserId có tồn tại không (sử dụng UserManager từ Identity)
            if (productUpdateDTO.UserId.HasValue)
            {
                var userExists = await _userManager.FindByIdAsync(productUpdateDTO.UserId.Value.ToString());
                if (userExists == null)
                {
                    throw new ArgumentException("User does not exist.");
                }
            }

            // Ánh xạ DTO sang entity
            _mapper.Map(productUpdateDTO, existingProduct);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
