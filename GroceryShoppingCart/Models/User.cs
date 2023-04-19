using System.ComponentModel.DataAnnotations;

namespace GroceryShoppingCartAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public long Phone_No { get; set; }
    }
}
