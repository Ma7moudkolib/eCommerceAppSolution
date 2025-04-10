using eCommerce.Application.Exceptions;
using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext _context): IGenericRepository<TEntity> where TEntity : class
    {
       
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync( entity );
           // return await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            var entity = await _context.Set<TEntity>().FindAsync( Id );
            if (entity == null)
            {
                throw new ItemNotFoundException($"Item With {Id} Not Found");
            }
            _context.Set<TEntity>().Remove( entity );
          //  return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var result = await _context.Set<TEntity>().FindAsync(id);
            return result!;
        }

        public void UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
           // return await _context.SaveChangesAsync();
        }
    }
}
