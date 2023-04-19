using System.ComponentModel.DataAnnotations;

namespace GroceryShoppingCartAPI.Models
{
    public class UserLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType("password")]
        public string Password { get; set; }
    }
}
