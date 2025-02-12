namespace eCommerce.Application.DTOs.Cart
{
    public class CreateAchieve
    {
        
        public required Guid ProductId { get; set; }
        public required int Quantity { get; set; }
        public required Guid UserId { get; set; }
    }
}
