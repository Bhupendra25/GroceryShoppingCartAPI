using System.ComponentModel;

namespace GroceryShoppingCartAPI.DTO
{
    public class EmailDto
    {
        [Description("telly.brakus@ethereal.email")]
        public string To { get; set; }
        [Description("Order Placed Successfully")]
        public string Subject { get; set; } //= "Order Placed Successfully";
        [Description("Your order has been recieved and will reach to you within 4 to 5 week Days ")]
        public string Body { get; set; } //= "Your order has been recieved and will reach to you within 4 to 5 week Days ";
    }
}
