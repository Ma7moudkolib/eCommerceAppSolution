namespace eCommerce.Application.DTOs.Cart
{
    public record GetPaymentMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
