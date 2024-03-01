using Microsoft.EntityFrameworkCore;
using NetProject.model;

namespace NetProject.Data
{
    public class RegisterUserContext : DbContext

    {
        public RegisterUserContext(DbContextOptions<RegisterUserContext> options) : base(options)
        {
        }

        public DbSet<RegisterUser> RegisterUsers { get; set; }
    }
}
