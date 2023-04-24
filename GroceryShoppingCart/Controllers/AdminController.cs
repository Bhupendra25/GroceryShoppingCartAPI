using GroceryShoppingCartAPI.Data;
using GroceryShoppingCartAPI.DTO;
using GroceryShoppingCartAPI.Models;
using GroceryShoppingCartAPI.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryShoppingCartAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly APIDbContext _context;
        private readonly IEmailService _emailService;
        public AdminController(APIDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        //[Authorize]
        [HttpGet]
        [Route("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            if (_context.products == null)
            {
                return NotFound();
            }
            return await _context.products.ToListAsync();
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<IEnumerable<Product>>> SearchProduct(int id)
        {
            if (_context.products == null)
            {
                return NotFound();
            }
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [Route("Add Product")]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.products.Where(n => n.PrName == product.PrName).Any())
            {
                return Ok("Product already Available");
            }
            else
            {
                _context.products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = product.PrID }, product);
            }
        }

        [HttpPut]
        [Route("Update Product")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.PrID)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }


            }
            return Ok();
        }

        private bool ProductAvailable(int id)
        {
            return (_context.products?.Any(p => p.PrID == id)).GetValueOrDefault();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (_context.products == null)
            {
                return NotFound();
            }
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost]
        [Route("Confirmation Email")]
        public IActionResult SendEmail()
        {
            var mail = new EmailDto
            {
                To = "katherine94@ethereal.email",
                Subject = "Order Placed Successfully",
                Body = "Your order has been recieved and will reach to you within 4 to 5 week Days "
            };
            _emailService.SendEmail(mail);

            return Ok();
        }
    }
}
