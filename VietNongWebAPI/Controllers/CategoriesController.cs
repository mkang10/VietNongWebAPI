using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VietNongWebAPI.DTO;
using VietNongWebAPI.Models;
using VietNongWebAPI.Repository;

namespace VietNongWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo) // Inject ICategoryRepo
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categoryDTOs = await _categoryRepo.GetAllAsync();
            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var categoryDTO = await _categoryRepo.GetByIdAsync(id);
            if (categoryDTO == null)
            {
                return NotFound(new { Message = "Category not found" });
            }
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryRepo.CreateAsync(categoryCreateDTO);
            if (result > 0)
            {
                return Ok(new { Message = "Category created successfully" });
            }

            return BadRequest(new { Message = "Failed to create category" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            if (id != categoryUpdateDTO.CategoryId)
            {
                return BadRequest(new { Message = "Invalid Category ID" });
            }

            var result = await _categoryRepo.UpdateAsync(categoryUpdateDTO);
            if (result > 0)
            {
                return Ok(new { Message = "Category updated successfully" });
            }

            return BadRequest(new { Message = "Failed to update category" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryRepo.RemoveAsync(id);
            if (result)
            {
                return Ok(new { Message = "Category deleted successfully" });
            }

            return BadRequest(new { Message = "Failed to delete category" });
        }
    }
}
