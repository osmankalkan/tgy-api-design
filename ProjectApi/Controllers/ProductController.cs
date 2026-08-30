using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Data;
using ProjectApi.DTOs;
using ProjectApi.Models;
using ProjectApi.Services;
namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _productService.GetByIdAsync(id);

            if (products == null)
            {
                return NotFound(new { message = "Ürün Bulunamadı" });
            }
            
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var product = await _productService.CreateAsync(request);
            
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            var succes = await _productService.UpdateAsync(id, request);
            if (!succes)
            {
                return NotFound(new { message = "Ürün Bulunamadı" });
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var succes = await _productService.DeleteAsync(id);
            if (!succes)
            {
                return NotFound("Ürün Bulunamadı");
            }
            return NoContent();
        }

    }

    public interface IActionResult<T>
    {
    }
}