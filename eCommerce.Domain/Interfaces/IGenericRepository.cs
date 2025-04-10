
namespace eCommerce.Domain.Interfaces
{
    public interface IGenericRepository<TEntity>where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid Id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
