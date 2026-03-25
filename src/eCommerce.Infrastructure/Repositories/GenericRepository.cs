using eCommerce.Domain.Interfaces;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerce.Infrastructure.Repositories
{
    public abstract class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        protected GenericRepository(AppDbContext dbContext)=> _context = dbContext;

        public void Create(T entity) => _context.Set<T>().Add(entity);
        public void Delete(T entity) => _context?.Set<T>().Remove(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? _context.Set<T>().AsNoTracking()
            : _context.Set<T>();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges) =>
        !trackChanges ? _context.Set<T>().Where(condition).AsNoTracking() : _context.Set<T>().Where(condition);
    }
}
