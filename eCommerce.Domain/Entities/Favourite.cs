using eCommerce.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain.Entities
{
    public class Favourite
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string? UserId { get; set; }
        public DateTime CreatedData { get; set; }= DateTime.Now;
        public ICollection<Product>? Products { get; set; }
        public AppUser? User { get; set; }

    }
}
