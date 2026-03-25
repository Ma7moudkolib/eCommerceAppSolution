using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Interfaces.UnitOfWork;
namespace eCommerce.Application.Services.Implementations.Cart
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;
        public PaymentMethodService(IRepositoryManager repositoryManager , IMapper mapper )
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods()
        {
           
           var methods = await _repositoryManager.PaymentMethod.GetPaymentMethodAsync();
            if (!methods.Any()) { return[];  };

            return  _mapper.Map<IEnumerable< GetPaymentMethod>>( methods );
        }
    }
}
