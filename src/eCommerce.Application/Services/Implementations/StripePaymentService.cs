using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using Stripe.Checkout;
namespace eCommerce.Application.Services.Implementations
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<ServiceResponse> Pay(decimal TotalAmount, IEnumerable<Product> cartproducts, IEnumerable<ProcessCart> Carts)
        {
            try
            {
                var lineItem = new List<SessionLineItemOptions>();
                foreach (var item in cartproducts)
                {
                    var pQuntity = Carts.FirstOrDefault(p => p.PoductId == item.Id);
                    lineItem.Add(new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Name,
                                Description = item.Description,
                            },
                            UnitAmount = (long)(item.Price * 100),

                        },
                        Quantity = pQuntity!.Quantity,
                    });
                }
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = ["usd"],
                    LineItems = lineItem,
                    Mode = "Payment",
                    SuccessUrl = "https:localhost:7278/Payment-Success",
                    CancelUrl = "https:localhost:7278/Payment-Cancel",
                };
                var service = new SessionService();
                Session session = await service.CreateAsync(options);
                return new ServiceResponse(true, session.Url);
            }
            catch (Exception ex) 
            {
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
