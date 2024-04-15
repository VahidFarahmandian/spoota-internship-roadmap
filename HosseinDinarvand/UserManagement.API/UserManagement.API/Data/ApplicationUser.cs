using Microsoft.AspNetCore.Identity;

namespace UserManagement.API.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
