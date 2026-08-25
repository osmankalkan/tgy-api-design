using Microsoft.AspNetCore.Mvc;
namespace ProjectApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : ControllerBase
    {
        private static List<string> products = new List<string>
        {
            "Laptop",
            "Telefon",
            "Kulaklık"
        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }  
        [HttpGet("{index}")]
        public IActionResult GetByIndex(int index)
        {
            if (index < 0 || index >= products.Count)
            {
                return NotFound("Ürün Bulunamadı");
            }
            return Ok(products[index]);
        }

        [HttpPost]
        public ActionResult< string> Create([FromBody] string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return BadRequest("Ürün Adı Boş Olamaz");
            }
            products.Add(productName);
            return CreatedAtAction(nameof(GetByIndex), new { index = products.Count - 1 }, "Ürün Eklendi");

        }

        [HttpPut("{index}")]
        public IActionResult Update(int index, [FromBody] string newName)
        {
            if (index < 0 || index >= products.Count)
            {
                return NotFound("Ürün Bulunamadı");
            }
            if (string.IsNullOrWhiteSpace(newName))
            {
                return BadRequest("Ürün Adı Boş Olamaz");
            }
            products[index] = newName;
            return Ok("Ürün Güncellendi");
        }

        [HttpDelete("{index}")]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= products.Count)
            {
                return NotFound("Ürün Bulunamadı");
            }
            products.RemoveAt(index);
            return Ok("Ürün Silindi");
        }

    }
}