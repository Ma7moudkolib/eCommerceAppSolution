using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart
{
    public class CreateAchieve
    {
        [Required]
        public required Guid ProductId { get; set; }
        [Required]
        public required int Quantity { get; set; }
        [Required]
        public  string? UserId { get; set; }
    }
}
