using GroceryShoppingCartAPI.Data;
using GroceryShoppingCartAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GroceryShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IConfiguration _config;
        private readonly APIDbContext _context;
        public UserController(IConfiguration config, APIDbContext context)
        {
            _config = config;
            _context = context;
        }


        /* private readonly APIDbContext _context;
         public UserController(APIDbContext context)
         {
             _context = context;
         }*/

        private User AuthenticateUser(UserLogin usr)
        {
            User _usr = null;
            var credentials = _context.users.Where(model => model.UserName == usr.UserName && model.Password == usr.Password).FirstOrDefault();

            if (credentials != null)

            {
                _usr = new User { UserName = usr.UserName };
            }

            /*  if (usr.UserName ==   && usr.Password == "Admin123")
              {
                  _usr = new User { UserName = "Bhupendra" };
              }*/
            return _usr;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserLogin usr)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(usr);
            if (user != null)
            {
                var token = GenerateToken(user);
                response = Ok(new { token = token });
            }
            return response;
        }



    }
}
