using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Interfaces.CategorySpecific
{
    public interface ICategory
    {
        Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId);
    }
}
