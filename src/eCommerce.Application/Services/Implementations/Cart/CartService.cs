using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.UnitOfWork;
namespace eCommerce.Application.Services.Implementations.Cart
{
    public class CartService : ICartService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;
        public CartService(IRepositoryManager repositoryManager , IMapper mapper , IPaymentService paymentService)
        {
            _mapper = mapper;
            _repositoryManager = repositoryManager;
            _paymentService = paymentService;
        }
        public async Task<ServiceResponse> Checkout(Checkout checkout,CancellationToken cancellationToken = default)
        {
            decimal total = 0;
            List<Product> itemSelected = new List<Product>();
            foreach (var item in checkout.Carts)
            {
                var product = await _repositoryManager.Product.GetProductByIdAsync(item.PoductId, true);
                if (product is null)
                    return new ServiceResponse(false, $"The product with Id:{item.PoductId} not available now.");

                if (product.Quantity < item.Quantity)
                    return new ServiceResponse(false, $"The quantity of product with id:{item.PoductId} not available now.");
                total += (item.Quantity * item.Price);
                itemSelected.Add(product);
                product.Quantity -= item.Quantity;
                _repositoryManager.Product.UpdateProduct(product);
            }

            var paymentMethod = await _repositoryManager.PaymentMethod.GetPaymentMethodAsync();
            if (checkout.ProcessMethodId != paymentMethod.FirstOrDefault()!.Id)
                return new ServiceResponse(false, "Invalid Payment Method");
            var result =  await _paymentService.Pay(total, itemSelected, checkout.Carts);
            if (result.Success && await _repositoryManager.CompleteAsync(cancellationToken: cancellationToken) > 0)
                return result;
            return new ServiceResponse(false, "Payment Failed");



        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves ,CancellationToken cancellationToken=default)
        {
            var mapModel = _mapper.Map<IEnumerable<Achieve>>(achieves);
         
            await _repositoryManager.Cart.SaveCheckoutHistory(mapModel);
            var result = await _repositoryManager.CompleteAsync(cancellationToken);
            if (result > 0)
            {   
                new ServiceResponse(true, "Checkout Achieve");
            }
            return new ServiceResponse(false, "Error occuered in Saving");
        }
    }
}
