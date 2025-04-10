using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces.Authentication;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Domain.Interfaces.CategorySpecific;
namespace eCommerce.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }
        ICategory Categories { get; }
        IGenericRepository<Favourite> Favorites { get; }
        ICart cart { get; }
        IPaymentMethod paymentMethod { get; }
        Task<int> CompleteAsync();
    }
}
