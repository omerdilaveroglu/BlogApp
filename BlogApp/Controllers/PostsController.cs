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
        public JsonResult AddComment(int PostId, string UserName, string CommentText)
        {
            User newUser = new User { UserName = UserName, Image = "1.jpg" };

            Comment entity = new Comment
            {
                CommentText = CommentText,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                User = newUser
            };

            _commentRepository.Add(entity);

            Console.WriteLine(entity.User.UserName.ToString()
            + Environment.NewLine + entity.CommentText.ToString() + Environment.NewLine + entity.PublishedOn.ToString()
            + Environment.NewLine + entity.User.Image.ToString());

            return Json(new
            {
                UserName,
                CommentText,
                entity.PublishedOn,
                entity.User.Image
                
            });
        }
    }
}