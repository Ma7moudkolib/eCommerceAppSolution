using eCommerce.Application.DTOs.Category;
using System.ComponentModel.DataAnnotations;
namespace eCommerce.Application.DTOs.Product
{
    public record GetProduct 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        
        public decimal Price { get; set; }
        
        public string Image { get; set; }
        
        public int Quantity { get; set; }
        
        public int CategoryId { get; set; }
    }
}

