using eCommerce.Application.DTOs.Category;
using System.ComponentModel.DataAnnotations;
namespace eCommerce.Application.DTOs.Product
{
    public class GetProduct : ProductBase
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategory? Category { get; set; }
    }
}

