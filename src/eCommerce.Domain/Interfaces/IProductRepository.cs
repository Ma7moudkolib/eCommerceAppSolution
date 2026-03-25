using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        Task<Product?> GetProductByIdAsync(int productId,bool trackChanges);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
        Task<IEnumerable<Product>> GetProductsByCategory(int categoryId, bool trackChanges);
    }
}
