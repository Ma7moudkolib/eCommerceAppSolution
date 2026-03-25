using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Product;
namespace eCommerce.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> AddProductAsync(CreateProduct product , CancellationToken cancellationToken = default);
        Task<ServiceResponse> UpdateProductAsync(UpdateProduct product , CancellationToken cancellationToken=default);
        Task<ServiceResponse> DeleteProductAsync(int Id , CancellationToken cancellationToken=default);
        Task<IEnumerable<GetProduct>> GetAllProductAsync();
        Task<GetProduct> GetProductByIdAsync(int id);
        Task<IEnumerable<GetProduct>> GetProductsByCategoryIdAsync(int categoryId);
    }
}
