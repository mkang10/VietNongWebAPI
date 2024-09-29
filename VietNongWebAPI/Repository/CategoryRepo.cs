using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VietNongWebAPI.DTO;
using VietNongWebAPI.Models;

namespace VietNongWebAPI.Repository
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(int id);
        Task<int> CreateAsync(CategoryCreateDTO categoryCreateDTO);
        Task<int> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO);
        Task<bool> RemoveAsync(int id);
    }
    public class CategoryRepo : ICategoryRepo
    {
        private readonly VietNongContext _context;
        private readonly IMapper _mapper;

        public CategoryRepo(VietNongContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAsync(CategoryCreateDTO categoryCreateDTO)
        {
            var category = _mapper.Map<Category>(categoryCreateDTO);
            _context.Categories.Add(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO)
        {
            var existingCategory = await _context.Categories.FindAsync(categoryUpdateDTO.CategoryId);
            if (existingCategory == null)
            {
                return 0;
            }

            _mapper.Map(categoryUpdateDTO, existingCategory);
            return await _context.SaveChangesAsync();
        }
    }
}
