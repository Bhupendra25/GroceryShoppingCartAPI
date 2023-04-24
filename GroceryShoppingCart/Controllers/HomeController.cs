using GroceryShoppingCartAPI.Data;
using GroceryShoppingCartAPI.DTO;
using GroceryShoppingCartAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly APIDbContext _context;

        public HomeController(APIDbContext context)
        {
            _context = context;

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            if (_context.products == null)
            {
                return NotFound();
            }
            return await _context.products.ToListAsync();
        }

        [HttpGet("{productname}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string productname)
        {
            if (_context.products == null)
            {
                return NotFound();
            }
            var product = await _context.products.FirstOrDefaultAsync(p => p.PrName == productname);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddToCart(AddToCart item)
        {
            var product = await _context.products.FirstOrDefaultAsync(p => p.PrName == item.ProductName);
            if (product == null)
            {
                return Ok("Product does not exist!!!");
            }
            var Newuser = new UserCart()
            {
                prName = item.ProductName,
                Username = item.Username,
                Quantity = item.Quantity,
                Amount = item.Quantity * product.Price,
            };
            if (_context.users.Where(u => u.UserName == item.Username).Any())
            {
                await _context.userCarts.AddAsync(Newuser);
                await _context.SaveChangesAsync();
                return Ok();

            }
            else
            {

                return Ok("User does not exist!!!");
            }
        }

        [HttpPost("BuyNow/{username}")]
        public async Task<ActionResult> BuyNow(string username)
        {
            // Get all products in user's cart
            var userCartItems = await _context.userCarts
                .Where(c => c.Username == username)
                .ToListAsync();

            if (userCartItems.Count == 0)
            {
                return Ok("No items in cart!");
            }

            // Calculate total amount due
            decimal totalAmount = userCartItems.Sum(c => c.Amount);

            // Merge cart items into a list of product names
            var products = userCartItems.Select(c => c.prName).ToList();


            // Return list of products and total amount due
            return Ok(new { products, totalAmount });
        }

        [HttpPost("Checkout")]
        public async Task<ActionResult> Checkout(string username, EmailDto request)
        {
            // Get all products in user's cart
            var userCartItems = await _context.userCarts
                .Where(c => c.Username == username)
                .ToListAsync();

            // Clear user's cart
            _context.userCarts.RemoveRange(userCartItems);
            await _context.SaveChangesAsync();

            foreach (var cartItem in userCartItems)
            {
                // Find corresponding product in Product table
                var product = await _context.products.FirstOrDefaultAsync(p => p.PrName == cartItem.prName);

                if (product != null)
                {
                    // Subtract buynow quantity from product quantity
                    product.Quantity -= cartItem.Quantity;
                }
            }

            // Save changes to Product table
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
