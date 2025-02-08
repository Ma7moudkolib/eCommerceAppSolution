using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Identity;
namespace eCommerce.Application.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task <ServiceResponse> CreateUser(CreateUser createUser);
        Task<LoginResponse> LoginUser(LoginUser login);
        Task <LoginResponse> RevivToken(string refreshToken);
    }
}
