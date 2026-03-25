using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    public sealed class CategoryRepository
        : GenericRepository<Category>, ICategory
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    
        public void CreateCategory(Category category) => Create(category);


        public void DeleteCategory(Category category) => Delete(category);

        public async Task<IEnumerable<Category>> GetAllCategories(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<Category?> GetCategoryById(int categoryId, bool trackChanges) =>
            await FindByCondition(c => c.Id == categoryId, trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateCategory(Category category) => Update(category);
    }
}
