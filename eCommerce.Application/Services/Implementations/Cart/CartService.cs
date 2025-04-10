using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.UnitOfWork;
namespace eCommerce.Application.Services.Implementations.Cart
{
    public class CartService(IMapper mapper , IPaymentService paymentService 
        , IUnitOfWork unitOfWork) : ICartService
    {
        public async Task<ServiceResponse> Checkout(Checkout checkout)
        {
            var (products, totalAmount) = await GetTotalAmound(checkout.Carts);
           // var paymentMethod = await payment.GetPaymentMethods();
           var paymentMethod = await unitOfWork.paymentMethod.GetPaymentMethodAsync();
            if (checkout.ProcessMethodId == paymentMethod.FirstOrDefault()!.Id)
            {
                return await paymentService.Pay(totalAmount, products, checkout.Carts);
            }
            return new ServiceResponse(false, "Invalid Payment Method");
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
        {
            var mapModel = mapper.Map<IEnumerable<Achieve>>(achieves);
          //  var result =  await cartRepository.SaveCheckoutHistory(mapModel);
            await unitOfWork.cart.SaveCheckoutHistory(mapModel);
            var result = await unitOfWork.CompleteAsync();
            if (result > 0)
            {   
                new ServiceResponse(true, "Checkout Achieve");
            }
            unitOfWork.Dispose();
            return new ServiceResponse(false, "Error occuered in Saving");
        }
        private async Task<(IEnumerable<Product> , decimal)> GetTotalAmound(IEnumerable<ProcessCart> carts)
        {
            if (!carts.Any()) { return ([], 0); }
            //var products = await repository.GetAllAsync();
            var products = await unitOfWork.Products.GetAllAsync();
            if (!products.Any()) { return ([], 0); }

            var cartsProducts = products.Select(cartItem=> products.FirstOrDefault(p=>p.Id == cartItem.Id))
                .Where(product=>product != null).ToList();
            var totalAmount = carts.Where(cartItem => cartsProducts.Any(p => p!.Id == cartItem.PoductId))
                .Sum(cartItem => cartItem.Quantity * cartsProducts.First(p => p!.Id == cartItem.PoductId)!.Price);
            return (cartsProducts!, totalAmount);

        }
    }
}
