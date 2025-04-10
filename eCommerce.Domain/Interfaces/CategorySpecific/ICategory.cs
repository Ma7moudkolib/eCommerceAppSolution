using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Interfaces.CategorySpecific
{
    public interface ICategory : IGenericRepository<Category>
    {
        Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId);
    }
}
