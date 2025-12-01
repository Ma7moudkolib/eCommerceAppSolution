using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart
{
    public class CreateAchieve
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public  string? UserId { get; set; }
    }
}
