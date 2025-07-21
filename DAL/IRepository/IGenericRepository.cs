using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(object id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T? FirstOrDefault(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        int Count(Expression<Func<T, bool>>? predicate = null);
        bool Exists(Expression<Func<T, bool>> predicate);
    }
}