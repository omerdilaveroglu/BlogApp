using System.Linq;
using System.Security.Claims;
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
        private IRepository<Comment> _commentRepository;

        public PostsController(IRepository<Post> postRepository,
                                 IRepository<Tag> tagRepository,
                                 IRepository<Comment> commentRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _commentRepository = commentRepository;
        }

        public async Task<IActionResult> Index(string? tag)
        {

            var claims = User.Claims;

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
            var post = await _postRepository.Items
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Url == url);

            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string CommentText)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var image = User.FindFirstValue(ClaimTypes.UserData);

            Comment entity = new Comment
            {
                CommentText = CommentText,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                UserId = int.Parse(userId ?? ""),
            };

            _commentRepository.Add(entity);

            return Json(new
            {
                userName,
                CommentText,
                entity.PublishedOn,
                image

            });
        }
    }
}