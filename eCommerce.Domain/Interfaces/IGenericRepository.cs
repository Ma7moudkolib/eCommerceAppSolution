
using System.Linq.Expressions;

namespace eCommerce.Domain.Interfaces
{
    public interface IGenericRepository<T>where T : class
    {

        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
