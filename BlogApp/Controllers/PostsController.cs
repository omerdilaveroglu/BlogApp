using System.Linq;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index(string? tag)
        {

            if (!string.IsNullOrEmpty(tag))
            {
                var taggedPosts = await _postRepository.Items
                    .Where(p => p.Tags.Any(t => t.Text == tag))
                    .ToListAsync();

                var viewModel = new PostsViewModel
                {
                    Posts = taggedPosts
                };

                return View(viewModel);
            }

            var posts = await _postRepository.GetListAsync();

            var allPostsViewModel = new PostsViewModel
            {
                Posts = posts 
            };

            return View(allPostsViewModel);
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