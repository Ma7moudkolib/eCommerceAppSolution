using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
namespace eCommerce.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse> AddAsync(CreateCategory category);
        Task<ServiceResponse> UpdateAsync(UpdateCategory category);
        Task<ServiceResponse> DeleteAsync(Guid Id);
        Task<IEnumerable<GetCategory>> GetAllAsync();
        Task<GetCategory> GetByIdAsync(Guid id);
    }
}
