using eCommerce.Domain.Entities.Cart;
namespace eCommerce.Domain.Interfaces.Cart
{
    public interface ICart
    {
        Task SaveCheckoutHistory(IEnumerable<Achieve> checkout);

    }
}
