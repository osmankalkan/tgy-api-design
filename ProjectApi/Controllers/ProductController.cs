using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApi.Data;
using ProjectApi.DTOs;
using ProjectApi.Models;
namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.Where(p => p.IsActive).Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
            }).ToListAsync();
            return Ok(products);
        }  
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null || !products.IsActive)
            {
                return NotFound(new { message = "Ürün Bulunamadı" });
            }
            var response = new ProductResponse
            {
                Id = products.Id,
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
                StockQuantity = products.StockQuantity,
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult<ProductResponse>> Create([FromBody] CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                IsActive = true
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var response = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
            };
            return (IActionResult<ProductResponse>)CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null || !product.IsActive)
            {
                return NotFound(new { message = "Ürün Bulunamadı" });
            }
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            await _context.SaveChangesAsync();
            return Ok("Ürün Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null || !product.IsActive)
            {
                return NotFound("Ürün Bulunamadı");
            }
            product.IsActive = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }

    public interface IActionResult<T>
    {
    }
}