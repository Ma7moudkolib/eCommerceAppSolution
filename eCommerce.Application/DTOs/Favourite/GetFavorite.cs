using eCommerce.Application.DTOs.Product;
namespace eCommerce.Application.DTOs.Favourite
{
    public class GetFavorite
    {
        public ICollection<GetProduct>? Products { get; set; }
    }
}
