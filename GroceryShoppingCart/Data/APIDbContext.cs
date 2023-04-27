using GroceryShoppingCartAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingCartAPI.Data
{
    public class APIDbContext : IdentityDbContext<ApplicationUser>
    {
        public APIDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> products { get; set; }
        public DbSet<UserCart> userCarts { get; set; }
    }
}
