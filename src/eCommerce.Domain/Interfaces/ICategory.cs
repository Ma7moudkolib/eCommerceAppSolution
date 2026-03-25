using eCommerce.Domain.Entities;

namespace eCommerce.Domain.Interfaces
{
    public interface ICategory
    {
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
        void UpdateCategory(Category category);

        Task<Category?> GetCategoryById(int categoryId, bool trackChanges);
        Task<IEnumerable<Category>> GetAllCategories(bool trackChanges);
    }
}
