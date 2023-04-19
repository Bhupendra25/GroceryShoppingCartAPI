using GroceryShoppingCartAPI.Data;
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

        /* [HttpGet]
         public IActionResult Index()
         {
             return Ok(_context.products.ToList());

         }*/

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
            var Newuser = new UserCart()
            {
                prName = item.ProductName,
                Quantity = item.Quantity,
                Amount = item.Quantity * product.Price,
            };
            await _context.userCarts.AddAsync(Newuser);
            await _context.SaveChangesAsync();


            return Ok();
        }

    }
}
