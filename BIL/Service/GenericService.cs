using System.Linq.Expressions;
using BIL.IServices;
using DAL.IRepositories;
using DAL.Repository;

public class GenericService<T> : IGenericService<T> where T : class
{
    protected readonly IGenericRepository<T> _repository;

    public GenericService()
    {
        _repository = new GenericRepository<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _repository.GetAll();
    }

    public T? GetById(object id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return _repository.GetAll().Where(predicate);
    }

    public T? FirstOrDefault(Func<T, bool> predicate)
    {
        return _repository.GetAll().FirstOrDefault(predicate);
    }

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _repository.Find(predicate);
    }

    public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return _repository.FirstOrDefault(predicate);
    }

    public void Add(T entity)
    {
        _repository.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _repository.AddRange(entities);
    }

    public void Update(T entity)
    {
        _repository.Update(entity);
    }

    public void Delete(object id)
    {
        var entity = _repository.GetById(id);
        if (entity != null)
        {
            _repository.Remove(entity);
        }
    }

    public bool Exists(Func<T, bool> predicate)
    {
        return _repository.GetAll().Any(predicate);
    }

    public int Count(Func<T, bool>? predicate = null)
    {
        return predicate == null
            ? _repository.GetAll().Count()
            : _repository.GetAll().Count(predicate);
    }

    public bool Exists(Expression<Func<T, bool>> predicate)
    {
        return _repository.Exists(predicate);
    }

    public int Count(Expression<Func<T, bool>>? predicate = null)
    {
        return _repository.Count(predicate);
    }
}
