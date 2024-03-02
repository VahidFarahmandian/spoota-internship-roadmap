using Newtonsoft.Json;
using System.Text;
using testingweb.Models;

namespace testingweb.Middleware
{
    public class ProductMiddleware
    {
        private readonly RequestDelegate _next;

        public ProductMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the request has a body
            if (context.Request.ContentLength.HasValue &&
                context.Request.ContentLength > 0 &&
                context.Request.ContentType != null &&
                context.Request.ContentType.Contains("application/json"))
            {
                // Read the request body
                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    var requestBody = await reader.ReadToEndAsync();
                    var product = JsonConvert.DeserializeObject<Product>(requestBody);

                    if (product != null && product.Id > 0)
                    {
                        var jsonProduct = JsonConvert.SerializeObject(product);

                        var byteArray = Encoding.UTF8.GetBytes(jsonProduct);


                        using (var stream = new MemoryStream(byteArray))
                        {
                            context.Request.Body = stream;

                            await _next(context);
                        }
                    }
                    else
                    {
                        // If the product ID is not valid, return a 405 Method Not Allowed response
                        context.Response.StatusCode = 405;
                        await context.Response.WriteAsync("Invalid product data id ");
                        return;
                    }
                }
            }

            // Call the next middleware in the pipeline.
            await _next(context);
        }
    }
        public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestProduct(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ProductMiddleware>();
        }
    }

}
