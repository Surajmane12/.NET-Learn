using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
namespace WebApplication2.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
      public DbSet<User> Users { set; get; }

      public DbSet<Product> Products { set; get; }
        
    }
}
