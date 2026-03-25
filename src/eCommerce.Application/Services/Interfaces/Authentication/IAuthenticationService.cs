using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Identity;
namespace eCommerce.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task <ServiceResponse> RegisterUser(CreateUser createUser);
        Task<LoginResponse> ValidateUser(LoginUser login);
        Task<LoginResponse> CreateToken();
    }
}
