using System.Linq;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Model; 
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
                Posts = _postRepository.Items.ToList()

            };

            return View(viewModel);
        }
        
        public async Task<IActionResult> Details(string url)
        {
            var post = await _postRepository.GetAsync(p => p.Url == url);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
    }
}