using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;
namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(CreateUser user)
        {
            var result = await authenticationService.RegisterUser(user);
            if(result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUser user)
        {
            var result = await authenticationService.ValidateUser(user);
            if (!result.Success)
                return BadRequest(result);
            var token = await authenticationService.CreateToken();
            return Ok(token);
        }
    }
}
