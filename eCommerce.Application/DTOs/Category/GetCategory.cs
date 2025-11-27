using eCommerce.Application.DTOs.Product;
using System.ComponentModel.DataAnnotations;
namespace eCommerce.Application.DTOs.Category
{
    public record GetCategory 
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

