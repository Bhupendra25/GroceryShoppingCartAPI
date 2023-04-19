using GroceryShoppingCartAPI.Data;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            return Ok(_context.products.ToList());

        }
    }
}
