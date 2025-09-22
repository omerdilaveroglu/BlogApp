using System;
using BlogApp.Entity;

namespace BlogApp.Data.Abstract;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> Posts { get; }
    void CreatePost(TEntity post);


}
