using System;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers;

public class PostsController : Controller
{
    private readonly IPostDal _postService;

    public PostsController(IPostDal postDal)
    {
        _postService = postDal;
    }
    public async Task<IActionResult> Index()
    {
        return View(await _postService.GetListAsync());
    }
}


