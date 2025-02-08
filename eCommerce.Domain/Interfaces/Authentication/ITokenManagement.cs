using System.Security.Claims;

namespace eCommerce.Domain.Interfaces.Authentication
{
    public interface ITokenManagement
    {
        string GetRefreshToken();
        List<Claim> GetUserClaimsFromToken(string token);
        Task<bool> ValidateRefreshToken(string refreshToken);
        Task<string>GetUserIdByRefreshToken(string refreshToken);
        Task<int> AddRefreshToken(string userId , string RefreshToken);
        Task<int> UpdateRefreshToken(string refreshToken);
        string GenerateToken(List<Claim> claims);
    }
}
