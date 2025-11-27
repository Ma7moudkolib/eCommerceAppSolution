using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;
namespace eCommerce.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse> AddCategoryAsync(CreateCategory category, CancellationToken cancellationToken=default);
        Task<ServiceResponse> UpdateCategoryAsync(UpdateCategory category , CancellationToken cancellationToken=default);
        Task<ServiceResponse> DeleteCategoryAsync(int Id , CancellationToken cancellationToken=default);
        Task<IEnumerable<GetCategory>> GetAllCategoryAsync();
        Task<GetCategory> GetCategoryByIdAsync(int id);
        
    }
   
}
