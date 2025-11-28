using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Domain.Interfaces.UnitOfWork;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Repositories.Cart;
namespace eCommerce.Infrastructure.UnitOfWorks
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext dbContext;
        private readonly  Lazy<IProductRepository>? productRepository;
        private readonly Lazy<ICategory>? categoryRepository;
        private readonly Lazy<ICart>? cartRepository;
        private readonly Lazy<IPaymentMethod>? paymentMethodRepository;
        public RepositoryManager(AppDbContext context)
        {
            dbContext = context;
            productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context));
            categoryRepository = new Lazy<ICategory>(() => new CategoryRepository(context));
            cartRepository = new Lazy<ICart>(() => new CartRepository(context));
            paymentMethodRepository = new Lazy<IPaymentMethod>(() => new PaymentMethodRepository(context));
        }

        public IProductRepository Product => productRepository!.Value;

        public ICategory Categorie => categoryRepository!.Value;

        public ICart Cart => cartRepository!.Value;

        public IPaymentMethod PaymentMethod => paymentMethodRepository!.Value;

        public async Task<int> CompleteAsync(CancellationToken cancellationToken) =>
            await dbContext.SaveChangesAsync(cancellationToken);
    }
}
