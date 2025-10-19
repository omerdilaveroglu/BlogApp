using BlogApp.Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace BlogApp.Data.Concrete.EfCore;

public class EfRepository<T,TContext> : IRepository<T> 
        where T : class, new() 
        where TContext : DbContext
{
    protected readonly BlogContext _context;

    public EfRepository(BlogContext context)
    {
        _context = context;
    }
    public IQueryable<T> Items => _context.Set<T>();

    public void Add(T entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }

    public T? Get(Expression<Func<T, bool>> filter)
    {
        return _context.Set<T>().Where(filter).FirstOrDefault();
    }

    public Task<T?> GetAsync(Expression<Func<T, bool>> filter)
    {
        return _context.Set<T>().Where(filter).FirstOrDefaultAsync();
    }

    public List<T> GetList(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.ToList();
    }

    public async Task<List<T>> GetListAsync(Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = _context.Set<T>();
        
        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync(); 
    }

    public void Update(T entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }
}
