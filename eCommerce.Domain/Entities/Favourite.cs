using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain.Entities
{
    public class Favourite
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid ProductId { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedData { get; set; } = DateTime.Now;
        Product? Product { get; set; }

    }
}
