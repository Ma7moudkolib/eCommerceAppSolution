using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Category
{
    public record CreateCategory 
    {
       public string Name { get; set; }
    }
}

