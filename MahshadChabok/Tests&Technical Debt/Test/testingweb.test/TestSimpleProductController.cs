using System.Web.Http.Results;
using testingweb.Models;
using testingweb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using testingweb.Repository;

namespace testingweb.test
{
    [TestClass]
    public class TestSimpleProductController
    {
        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetAllProducts() as List<Product>;
            Assert.IsNotNull(result);
            Assert.AreEqual(testProducts.Count, result.Count);
        }

        [TestMethod]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
           

            var controller = new SimpleProductController(GetTestProducts());

            // Act
            var result = await controller.GetAllProductsAsync();

            // Assert
            Assert.IsNotNull(result);
            var products = result.ToList();
            Assert.AreEqual(GetTestProducts().Count, products.Count);
        }
        [TestMethod]
        public void GetProduct_ShouldReturnOkWithProduct()
        {
            // Arrange
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            // Act
            var result = controller.GetProduct(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(2, ((Product)okResult.Value).Id);

        }

        [TestMethod]
        public void GetProduct_ShouldReturnNotFound()
        {
            // Arrange
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            // Act
            var result = controller.GetProduct(999); 
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));

           
        }

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnOkWithProduct()
        {
            // Arrange
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            // Act
            var result = await controller.GetProductAsync(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));

            var okResult = (OkObjectResult)result;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(Product));
            var product = okResult.Value as Product;
            Assert.IsNotNull(product, "The returned value is not of type Product");

        }

        [TestMethod]
        public async Task GetProductAsync_ShouldReturnNotFound()
        {
            // Arrange
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            // Act
            var result = await controller.GetProductAsync(999); 
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));

        }

        private List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>();
            testProducts.Add(new Product { Id = 0, Name = "Demo1", Price = 1 });
            testProducts.Add(new Product { Id = 1, Name = "Demo2", Price = 3.75M });
            testProducts.Add(new Product { Id = 2, Name = "Demo3", Price = 16.99M });
            testProducts.Add(new Product { Id = 3, Name = "Demo4", Price = 11.00M });

            return testProducts;
        }
    }
}
