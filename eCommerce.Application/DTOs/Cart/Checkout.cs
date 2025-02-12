namespace eCommerce.Application.DTOs.Cart
{
    public class Checkout
    {
        public required Guid ProcessMethodId { get; set; }
        public required IEnumerable<ProcessCart> Carts { get; set; }

    }
}
