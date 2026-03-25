using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController(IPaymentMethodService paymentMethod) : ControllerBase
    {
        [HttpGet]
      public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
        {
            var methods = await paymentMethod.GetPaymentMethods();
            if (!methods.Any())
                return NotFound();
            return Ok(methods);

            
        }
    }
}
