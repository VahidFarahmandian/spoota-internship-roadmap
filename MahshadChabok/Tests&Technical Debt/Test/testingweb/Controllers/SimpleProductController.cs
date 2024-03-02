using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers; // Add this using statement
using testingweb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace testingweb.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class SimpleProductController : ControllerBase
    {
        private readonly List<Product> products;

       
       
        public SimpleProductController(List<Product> products)
        {
            this.products = products;
            

        }

        [HttpGet("GetAllProducts")]
        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        [HttpGet("GetAllProductsAsync")]
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await Task.FromResult(GetAllProducts());
        }

        [HttpGet("GetProduct/{id}")]
        public IActionResult GetProduct(int id)
        {
            Product product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("GetProductAsync/{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            return await Task.FromResult(GetProduct(id));
        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Invalid product data it is null");
            }
            int newProductId = products.Count + 1;

            newProduct.Id = newProductId;
            products.Add(newProduct);
            return Ok(newProductId);
        }

    }
}
