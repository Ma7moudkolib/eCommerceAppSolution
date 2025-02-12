using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace eCommerce.Infrastructure.Repositories.Cart
{
    public class PaymentMethodRepository(AppDbContext context) : IPaymentMethod
    {
        public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodAsync()
        {
            return await context.PaymentMethods.AsNoTracking().ToListAsync();
        }
    }
}
