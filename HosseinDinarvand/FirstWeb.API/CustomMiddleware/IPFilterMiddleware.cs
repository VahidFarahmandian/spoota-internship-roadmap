using System.Net;

namespace FirstWeb.API.CustomMiddleware
{
    public class IPFilterMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;

        public IPFilterMiddleware(RequestDelegate _next, IConfiguration _configuration)
        {
            this.next = _next;
            this.configuration = _configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var allowedIPs = configuration.GetSection("AllowedIPs").Get<string[]>();
            var ipAddress = context.Connection.RemoteIpAddress;

            if (!ISIPAllowed(ipAddress!, allowedIPs!))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            await next(context);
        }

        private bool ISIPAllowed(IPAddress iPAddress, string[] allowedIPs)
        {
            foreach (var allowedIP in allowedIPs)
            {
                if (IPAddress.TryParse(allowedIP, out IPAddress? parsedIP))
                {
                    if (parsedIP.Equals(iPAddress))
                        return true;
                }
            }

            return false;
        }
    }
}
