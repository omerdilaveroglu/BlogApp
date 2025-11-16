using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Model;
using BlogApp.Entity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Posts");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Posts");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
                if (User?.Identity?.IsAuthenticated == true)
                {
                    return RedirectToAction("Index", "Posts");
                }

                if(model.Password != model.Password2)
                {
                    ModelState.AddModelError("", "Parolalar eşleşmiyor.");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == model.Email);
                    if (isUser == null)
                    {
                        User newUser = new User
                        {
                            UserName = model.UserName,
                            Name = model.Name,
                            Email = model.Email,
                            Password = model.Password,
                            Image = "default.png"
                        };
                        _userRepository.CreateUser(newUser);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bu email zaten kayıtlı.");
                    }
                }

                return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewmodel model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                    if (isUser != null)
                    {
                        var userClaims = new List<System.Security.Claims.Claim>();
                        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                        userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                        userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                        userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                        if (isUser.Email == "omer@omer.com")
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        }
                        var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };

                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        return RedirectToAction("Index", "Posts");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Geçersiz email veya şifre.");
                    }
                }
            }
            return View();
        }


    }
}
