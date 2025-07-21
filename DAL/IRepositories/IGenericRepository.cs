using System.Linq.Expressions;

namespace DAL.IRepositories;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}