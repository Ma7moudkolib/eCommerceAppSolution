using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(Checkout checkout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await cartService.Checkout(checkout);
            return result.Success? Ok(result) : BadRequest(result);
        }
        [HttpPost("savecheckout")]
        public async Task<IActionResult> SaveCheckout(IEnumerable<CreateAchieve> achieves)
        {
            var result = await cartService.SaveCheckoutHistory(achieves);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
