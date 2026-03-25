using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    public sealed class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }
        public void AddProduct(Product product) => Create(product);

        public void DeleteProduct(Product product) => Delete(product);

        public void UpdateProduct(Product product) => Update(product);

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(p=>p.Name).ToListAsync();

        public async Task<Product?> GetProductByIdAsync(int productId, bool trackChanges) =>
            await FindByCondition(p => p.Id == productId, trackChanges)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId, bool trackChanges) =>
            await FindByCondition(p => p.CategoryId == categoryId, trackChanges)
            .ToListAsync();

    }
}
