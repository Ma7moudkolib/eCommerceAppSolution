using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
namespace eCommerce.Application.Services.Interfaces.Cart
{
    public interface ICartService
    {
        Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves);
        Task <ServiceResponse> Checkout(Checkout checkout);
    }
}
