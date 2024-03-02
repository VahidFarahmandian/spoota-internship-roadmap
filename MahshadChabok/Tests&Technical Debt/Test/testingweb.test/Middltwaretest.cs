using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using testingweb.Middleware;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using testingweb.Models;
using Microsoft.AspNetCore.Http;
using Moq;
namespace testingweb.test
{ 
[TestClass]
    public class Middltwaretest
    {
        // HttpClient :

        [TestMethod]
        public async Task MiddlewareTest_ReturnsBadRequestForInvalidProduct()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()

                .Configure(app =>
                {
                   // app.UseRouting();
                    app.UseRequestProduct();
                    app.Run(async context =>
                    {
                        // Your final middleware or application logic here
                    });
                }) ;

            using var server = new TestServer(hostBuilder);
            using var client = server.CreateClient();

            // Create an invalid JSON representing a product with Id <= 0
            Product newProduct = new Product
            {
                Id = 0,
                Name = "New Product",
                Price = 29
            };
            var jsonProduct = JsonConvert.SerializeObject(newProduct);
            // Act
            var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/", content);


            // Assert
            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, response.StatusCode);

           
           var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(responseContent);
          
        }
        [TestMethod]
        public async Task MiddlewareTest_ReturnsOkForValidProduct()
        {
            // Arrange
            var hostBuilder = new WebHostBuilder()
                .Configure(app =>
                {
                    app.UseRequestProduct();
                    app.Run(async context =>
                    {
                       
                    });
                });

            using var server = new TestServer(hostBuilder);
            using var client = server.CreateClient();

            // Create a valid JSON representing a product with Id > 0
            var validProductJson = "{\"Id\": 1, \"Name\": \"ValidProduct\", \"Price\": 19.99}";

            // Act
            var content = new StringContent(validProductJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/", content);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

           
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(responseContent);
            
           

        }

        //HttpContext , MOQ :


        [TestMethod]
        public async Task Middleware_ValidProduct_ReturnsOk()
        {
            // USE MOQ
            // Arrange
            var nextMock = new Mock<RequestDelegate>();
            var middleware = new ProductMiddleware(nextMock.Object);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = HttpMethods.Post;
            httpContext.Request.ContentType = "application/json";
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{\"Id\": 1, \"Name\": \"Product\", \"Price\": 10.99}"));
            // Act
            await middleware.InvokeAsync(httpContext);

            // Assert
            Assert.AreEqual(StatusCodes.Status200OK, httpContext.Response.StatusCode);
           
            nextMock.Verify(next => next(It.IsAny<HttpContext>()), Times.Once);
        }

        [TestMethod]
        public async Task Middleware_InvalidProduct_ReturnsMethodNotAllowed()
        {
            // Arrange
            var middleware = new ProductMiddleware(next: context => Task.CompletedTask);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Method = HttpMethods.Post;
            httpContext.Request.ContentType = "application/json";
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes("{\"Id\": 0, \"Name\": \"InvalidProduct\", \"Price\": 9.99}"));

            // Act
            await middleware.InvokeAsync(httpContext);

            // Assert
            Assert.AreEqual(StatusCodes.Status405MethodNotAllowed, httpContext.Response.StatusCode);
          
        }

        [TestMethod]
        public async Task Middleware_NoRequestBody_ReturnsNextMiddleware()
        {
            // USE MOQ

            // Arrange
            var nextMock = new Mock<RequestDelegate>();
            var middleware = new ProductMiddleware(nextMock.Object);
            var httpContext = new DefaultHttpContext();
             httpContext.Request.Method = HttpMethods.Get;

            // Act
            await middleware.InvokeAsync(httpContext);

            // Assert
           
            nextMock.Verify(next => next(It.IsAny<HttpContext>()), Times.Once);
        }

    }
}
