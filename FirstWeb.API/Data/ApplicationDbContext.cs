using FirstWeb.API.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace FirstWeb.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {


        }

        public DbSet<Product> Products { get; set; }
    }
}
