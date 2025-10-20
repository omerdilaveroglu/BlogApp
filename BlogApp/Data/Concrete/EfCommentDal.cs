using System;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Data.Abstract;

namespace BlogApp.Data.Concrete;

public class EfCommentDal: EfRepository<Comment,BlogContext>, ICommentDal
{
    public EfCommentDal(BlogContext CommentContext) : base(CommentContext)
    {
        
    }

}
