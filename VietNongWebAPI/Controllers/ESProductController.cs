using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using VietNongWebAPI;
using VietNongWebAPI.Models;

namespace VietNongWebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ESProductController : ControllerBase
    {
        private readonly IElasticsearchService<Product> _elasticsearchService;

        public ESProductController(IElasticsearchService<Product> elasticsearchService)
        {
            this._elasticsearchService = elasticsearchService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var response = await _elasticsearchService.GetAllDocumentsAsync();
            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] Product product)
        {
            var result = await _elasticsearchService.CreateDocumentAsync(product);
            return Ok(result);

        }

        [HttpGet]
        [Route("read/{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var document = await _elasticsearchService.GetDocumentAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateDocument([FromBody] Product product)
        {
            var result = await _elasticsearchService.UpdateDocumentAsync(product);
            return Ok(result);
        }
    }
}
