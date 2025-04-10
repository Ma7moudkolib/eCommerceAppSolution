using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Domain.Interfaces.CategorySpecific;
using eCommerce.Domain.Interfaces.UnitOfWork;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Repositories.Cart;
using eCommerce.Infrastructure.Repositories.CategorySpecific;
namespace eCommerce.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        public IGenericRepository<Product> Products { get; private set; }
        public ICategory Categories { get; private set; }
        public IGenericRepository<Favourite> Favorites { get; private set; }
        public ICart cart { get; private set; }
        public IPaymentMethod paymentMethod { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            dbContext = context;
            Products = new GenericRepository<Product>(dbContext);
            Categories = new CategoryRepository(dbContext);
            Favorites = new GenericRepository<Favourite>(dbContext);
            cart = new CartRepository(dbContext);
            paymentMethod = new PaymentMethodRepository(dbContext);
        }
        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
           dbContext.Dispose();
        }
    }
}
