using Microsoft.AspNetCore.Mvc;
using testingweb.Models;

namespace testingweb.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<int> AddProductAsync(Product newProduct);
        //IActionResult GetProduct(int id);
        IEnumerable<Product> GetAllProducts();


    }
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository(List<Product> products)
        {
            _products = products ?? throw new ArgumentNullException(nameof(products));
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            // Simulate an asynchronous operation
            await Task.Delay(1);
            return _products;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            // Simulate an asynchronous operation
            await Task.Delay(1);
            return _products.FirstOrDefault(p => p.Id == productId);
        }
        
        public async Task<int> AddProductAsync(Product newProduct)
        {
            // Simulate an asynchronous operation
            await Task.Delay(1);

            int newProductId = _products.Count + 1;
            newProduct.Id = newProductId;
            _products.Add(newProduct);

            return newProductId;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }
    }
}
