using System;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Model;

public class NewPosts : ViewComponent
{
    private IRepository<Post> _postRepository;
    public NewPosts(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var postsList = await _postRepository.GetListAsync();
        var posts = postsList.OrderByDescending(p => p.PublishedOn).Take(5).ToList();
        return View(posts);
    }

}
