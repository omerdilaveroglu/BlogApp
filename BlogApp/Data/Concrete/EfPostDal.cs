using System;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete;

public class EfPostDal : EfRepository<Post,BlogContext>,IPostDal
{
    public EfPostDal(BlogContext blogContext) : base(blogContext)
    {
        
    }

}
