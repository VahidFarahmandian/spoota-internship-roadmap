using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetProject.Controllers
{
    [Route("api")]
    public class AuthController : Controller
    {
        [HttpGet("callback")]
        public async Task<IActionResult> Callback()
        {
            var result = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                // Authentication successful
                // Handle user claims and any additional logic

                return Ok(new { message = "Authentication successful" });
            }

            return Unauthorized(new { message = "Authentication failed" });
        }
    }
}
