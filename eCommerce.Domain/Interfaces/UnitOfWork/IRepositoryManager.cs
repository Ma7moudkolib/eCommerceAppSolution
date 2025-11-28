using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces.Authentication;
using eCommerce.Domain.Interfaces.Cart;
namespace eCommerce.Domain.Interfaces.UnitOfWork
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        ICategory Categorie { get; }
        ICart Cart { get; }
        IPaymentMethod PaymentMethod { get; }
        Task<int> CompleteAsync(CancellationToken cancellationToken);
    }
}
