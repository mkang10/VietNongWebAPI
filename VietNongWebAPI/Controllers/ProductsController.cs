using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;

        public ProductController(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var productDTOs = await _productRepo.GetAllAsync();
            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var productDTO = await _productRepo.GetByIdAsync(id);
            if (productDTO == null)
            {
                return NotFound(new { Message = "Product not found" });
            }
            return Ok(productDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDTO productCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _productRepo.CreateAsync(productCreateDTO);
                if (result > 0)
                {
                    return Ok(new { Message = "Product created successfully" });
                }

                return BadRequest(new { Message = "Failed to create product" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
        {
            if (id != productUpdateDTO.ProductId)
            {
                return BadRequest(new { Message = "Invalid Product ID" });
            }

            try
            {
                var result = await _productRepo.UpdateAsync(productUpdateDTO);
                if (result > 0)
                {
                    return Ok(new { Message = "Product updated successfully" });
                }

                return BadRequest(new { Message = "Failed to update product" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepo.RemoveAsync(id);
            if (result)
            {
                return Ok(new { Message = "Product deleted successfully" });
            }

            return BadRequest(new { Message = "Failed to delete product" });
        }
    }
}
