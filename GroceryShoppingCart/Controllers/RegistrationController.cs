using GroceryShoppingCartAPI.Data;
using GroceryShoppingCartAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly APIDbContext _context;
        public RegistrationController(APIDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> NewRegistration(UserRegistration usr)
        {
            var Newuser = new User()
            {
                UserName = usr.UserName,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                Address = usr.Address,
                Password = usr.Password,
                Phone_No = usr.Phone_No
            };
            await _context.users.AddAsync(Newuser);
            await _context.SaveChangesAsync();

            return Ok(Newuser);

        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_context.users.ToList());

        }
    }
}
