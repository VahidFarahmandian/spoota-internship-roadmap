using Microsoft.AspNetCore.Identity;

namespace FirstWeb.API.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
