using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace testingweb.Middleware
{
    public class SampleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpClient _httpClient;

        public SampleMiddleware(RequestDelegate next, HttpClient httpClient)
        {
            _next = next;
            _httpClient = httpClient;
        }

        public async Task Invoke(HttpContext context)
        {
            // Your middleware logic here
            var apiResponse = await _httpClient.GetStringAsync("https://example.com/api/data");
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(apiResponse);
            await _next(context);
        }
    }
}
