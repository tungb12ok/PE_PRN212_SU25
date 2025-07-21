using System.Linq.Expressions;

namespace BIL.IServices;

public interface IGenericService<T> where T : class
{
    IEnumerable<T> GetAll();
    T? GetById(object id);
    IEnumerable<T> Find(Func<T, bool> predicate);
    T? FirstOrDefault(Func<T, bool> predicate);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(object id);
    bool Exists(Func<T, bool> predicate);
    int Count(Func<T, bool>? predicate = null);
}