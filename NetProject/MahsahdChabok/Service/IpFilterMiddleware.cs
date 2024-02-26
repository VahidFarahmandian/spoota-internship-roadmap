using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace NetProject.Service
{
    public interface IIpFilterMiddleware
    {
        Task Invoke(HttpContext context);
    }

    public class IpFilterMiddleware : IIpFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public IpFilterMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string[] AllowedIpAddresses { get; set; }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;
            var allowedIpAddresses = _configuration.GetSection("AllowedIpAddresses").Get<string[]>();

            Console.WriteLine($"Incoming IP Address: {ipAddress}");
            Console.WriteLine($"Allowed IP Addresses: {string.Join(", ", allowedIpAddresses)}");

            if (!IsIpAddressAllowed(ipAddress))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync("Access Forbidden. Your IP address is not allowed.");
                return;
            }

            await _next(context);
        }

        private bool IsIpAddressAllowed(IPAddress ipAddress)
        {
            var allowedIpAddresses = _configuration.GetSection("AllowedIpAddresses").Get<string[]>();

            return allowedIpAddresses?.Any(allowedIp => IPAddress.Parse(allowedIp).Equals(ipAddress)) ?? false;
        }
    }
}

