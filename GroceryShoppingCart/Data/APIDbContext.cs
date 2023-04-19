using GroceryShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingCartAPI.Data
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
