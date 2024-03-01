
using Microsoft.EntityFrameworkCore;
using NetProject.model;

namespace NetProject.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
    }
}
