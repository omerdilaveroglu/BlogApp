using System.Linq;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Model; // PostsViewModel için gerekli
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class PostsController : Controller
    {
        private IRepository<Post> _postRepository;
        private IRepository<Tag> _tagRepository;

        public PostsController(IRepository<Post> postRepository, IRepository<Tag> tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new PostsViewModel
            {
                Posts = _postRepository.Posts.ToList(),
                Tags = _tagRepository.Posts.ToList() 
            };
            
            return View(viewModel);
        }
    }
}