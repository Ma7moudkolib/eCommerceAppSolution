using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart
{
    public class Checkout
    {
        [Required]
        public int ProcessMethodId { get; set; }
        [Required]
        public IEnumerable<ProcessCart> Carts { get; set; }

    }
}
