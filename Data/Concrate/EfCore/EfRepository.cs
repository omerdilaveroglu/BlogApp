using System;
using System.Linq;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrate.EfCore;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrate.EfCore;

public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly BlogContext _context;

    public EFRepository(BlogContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> Posts => _context.Set<TEntity>();

    public void CreatePost(TEntity post)
    {
        _context.Set<TEntity>().Add(post);
        _context.SaveChanges();
    }
}