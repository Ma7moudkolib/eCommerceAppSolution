using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Favourite;
namespace eCommerce.Application.Services.Interfaces
{
    public interface IFavouriteService
    {
        Task<ServiceResponse> AddAsync(CreateFavourite favourite);
        Task<ServiceResponse> DeleteAsync(Guid Id);
        Task<IEnumerable<GetFavorite>> GetAllAsync();
        Task<GetFavorite> GetByIdAsync(Guid id);
    }
}
