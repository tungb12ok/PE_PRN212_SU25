using System.Linq.Expressions;
using DAL.Entities;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository;

public class GenericRepository<T> where T : class
{
    protected readonly Su25researchDbContext _context;

    public GenericRepository()
    {
        _context = new Su25researchDbContext();
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(object id)
    {
        return _context.Set<T>().Find(id);
    }

    public IEnumerable<T> Find(Func<T, bool> predicate)
    {
        return _context.Set<T>().Where(predicate).ToList();
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }

    public int Count()
    {
        return _context.Set<T>().Count();
    }

    public bool Exists(Func<T, bool> predicate)
    {
        return _context.Set<T>().Any(predicate);
    }
}
