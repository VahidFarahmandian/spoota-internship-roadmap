using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetProject.model;

namespace NetProject.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions <UserDbContext> options): base(options)
        {
            
        }
        public DbSet<User> MyProperty { get; set; }
    }
}
