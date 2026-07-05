using Microsoft.AspNetCore.Mvc;
using ProductsApi.Models;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // In-memory storage
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 25000, Stock = 10 },
            new Product { Id = 2, Name = "Mouse", Price = 500, Stock = 50 }
        };

        private static int nextId = 3;

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            product.Id = nextId++;
            products.Add(product);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product
            );
        }

        // PUT: api/products/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, Product updatedProduct)
        {
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Stock = updatedProduct.Stock;

            return Ok(existingProduct);
        }

        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound();

            products.Remove(product);

            return NoContent();
        }
    }
}