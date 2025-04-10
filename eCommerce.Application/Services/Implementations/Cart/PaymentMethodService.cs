using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Domain.Interfaces.UnitOfWork;
namespace eCommerce.Application.Services.Implementations.Cart
{
    public class PaymentMethodService(IUnitOfWork unitOfWork ,IMapper mapper ) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods()
        {
           // var methods = await paymentMethod.GetPaymentMethodAsync();
           var methods = await unitOfWork.paymentMethod.GetPaymentMethodAsync();
            if (!methods.Any()) { return[];  };

            return  mapper.Map<IEnumerable< GetPaymentMethod>>( methods );
        }
    }
}
