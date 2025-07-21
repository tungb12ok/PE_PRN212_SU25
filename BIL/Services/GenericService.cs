using System.Linq.Expressions;
using BIL.IServices;
using DAL.Entities;
using DAL.IRepositories;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace BIL.Service;

public class GenericService<T> where T : class
{
    protected GenericRepository<T> _repository;

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

    public IEnumerable<T> Filter(Func<T, bool> predicate)
    {
        return _repository.Find(predicate);
    }

    public void Add(T entity)
    {
        _repository.Add(entity);
    }

    public void Update(T entity)
    {
        _repository.Update(entity);
    }

    public void Delete(T entity)
    {
        _repository.Delete(entity);
    }

    public int Count()
    {
        return _repository.Count();
    }

    public bool Exists(Func<T, bool> predicate)
    {
        return _repository.Exists(predicate);
    }
}
