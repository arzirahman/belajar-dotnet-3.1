using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using Coba_Net.Utils;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Filter;
using Microsoft.EntityFrameworkCore;
using Coba_Net.Services;

namespace Coba_Net.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDb _context;
        private readonly Jwt _jwt;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserService _service;

        public UserController(IWebHostEnvironment webHostEnvironment, ILogger<UserController> logger, AppDb context, Jwt jwt, UserService service)
        {
            _logger = logger;
            _context = context;
            _jwt = jwt;
            _webHostEnvironment = webHostEnvironment;
            _service = service;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            ViewData["ReturnUrl"] = ReturnUrl;
            return View(new Login());
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null || !user.VerifyPassword(login.Password))
            {
                var error = new Login{
                    ErrorMessage = "Wrong username or password"
                };
                return View(error);
            }
            var claimsIdentity = _jwt.GetClaimsPrincipal(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsIdentity);
            return Redirect(login.ReturnUrl ?? "/Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/User/Login");
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _context.User.FirstOrDefaultAsync(user => user.Email == ViewData["Email"] as string);
            if (TempData.TryGetValue("Message", out var message))
            {
                ViewData["Message"] = message;
            }
            return View(user);
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        [HttpPost]
        public async Task<IActionResult> Profile(IFormFile file, string name, string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(user => user.Email == ViewData["Email"] as string);
            var validator = new ProfileValidator(_context);
            var (isFormValid, errors) = await validator.ValidateProfile(user, file, name, email);
            if (isFormValid)
            {
                await _service.Profile(user, file, name, email);
                TempData["Message"] = "Profile edited successfully";
                return RedirectToAction("Profile", "User");
            }
            else{
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
                user.Email = email;
                user.Name = name;
                return View(user);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
