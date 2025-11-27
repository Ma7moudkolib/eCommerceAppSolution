using System.ComponentModel.DataAnnotations;
namespace eCommerce.Application.DTOs.Category
{
    public record UpdateCategory 
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

