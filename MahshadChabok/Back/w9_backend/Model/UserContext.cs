using Microsoft.EntityFrameworkCore;

namespace w9_backend.Model
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<User2FA> User2FAs { get; set; }
        public DbSet<UserOIDC> UserOIDCs { get; set; }

        public UserContext(DbContextOptions options) : base(options)
        {

        }
    }
}
