using System;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
namespace BlogApp.Data.Concrete;

public class EfTagDal : EfRepository<Tag, BlogContext>, ITagDal
{
    public EfTagDal(BlogContext blogContext) : base(blogContext)
    {
        
    }
}
