namespace GroceryShoppingCartAPI.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(DTO.EmailDto request);
    }
}
