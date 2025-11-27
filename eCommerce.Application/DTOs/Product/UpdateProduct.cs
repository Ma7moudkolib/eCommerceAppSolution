using System.ComponentModel.DataAnnotations;
namespace eCommerce.Application.DTOs.Product
{
    public record UpdateProduct:ProductBase
    {
        [Required]
        public int Id { get; set; }
 
    }
}

