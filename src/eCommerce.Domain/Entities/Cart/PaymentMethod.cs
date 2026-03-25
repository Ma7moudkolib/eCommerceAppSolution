using System.ComponentModel.DataAnnotations;
namespace eCommerce.Domain.Entities.Cart
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

    }
}
