using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Product;
namespace eCommerce.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> AddAsync(CreateProduct product);
        Task<ServiceResponse> UpdateAsync(UpdateProduct product);
        Task<ServiceResponse> DeleteAsync(Guid Id);
        Task<IEnumerable<GetProduct>> GetAllAsync();
        Task<GetProduct> GetByIdAsync(Guid id);
    }
}
