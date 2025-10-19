using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogApp.Data.Abstract;

public interface IRepository<T> where T : class, new()
{

    IQueryable<T> Items { get; } 
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    T? Get(Expression<Func<T, bool>> filter);
    Task<T?> GetAsync(Expression<Func<T, bool>> filter);
    Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null);
    List<T> GetList(Expression<Func<T, bool>>? filter = null);
}
