using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using Coba_Net.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Coba_Net.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDb _context;
        private readonly Jwt _jwt;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IWebHostEnvironment webHostEnvironment, ILogger<UserController> logger, AppDb context, Jwt jwt)
        {
            _logger = logger;
            _context = context;
            _jwt = jwt;
            _webHostEnvironment = webHostEnvironment;
        }

        private bool ValidateToken(out User userInfo)
        {
            userInfo = _jwt.ValidateToken(User.Claims.FirstOrDefault(c => c.Type == "Jwt")?.Value);
            if (userInfo == null){
                return false;
            }
            else{
                ViewData["Email"] = userInfo.Email;
                ViewData["Name"] = userInfo.Name;
                ViewData["PpUrl"] = userInfo.PpUrl;
                ViewData["Role"] = userInfo.Role;
                return true;
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/Home");
            }
            return View(new Login());
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login, string ReturnUrl)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == login.Email);
            if (user == null || !user.VerifyPassword(login.Password))
            {
                var error = new Login{
                    ErrorMessage = "Wrong username or password"
                };
                return View(error);
            }
            var claimsIdentity = _jwt.GetClaimsPrincipal(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsIdentity);
            return Redirect("/Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/User/Login");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            if (!ValidateToken(out User userInfo)) return Redirect("/User/Login");
            var user = _context.User.FirstOrDefault(user => user.Email == userInfo.Email);
            if (TempData.TryGetValue("Message", out var message))
            {
                ViewData["Message"] = message;
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(IFormFile file, string name, string email)
        {
            if (!ValidateToken(out User userInfo)) return Redirect("/User/Login");
            var user = _context.User.FirstOrDefault(user => user.Email == (string) userInfo.Email);
            var validator = new ProfileValidator(_context);
            if (validator.ValidateProfile(user, file, name, email, out Dictionary<string, string> errors))
            {
                if (file != null && file.Length > 0)
                {
                    string extension = Path.GetExtension(file.FileName);
                    string fileName = user.Id.ToString() + extension;
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "pp");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    user.PpUrl = "/pp/" + fileName;
                }
                user.Name = name;
                user.Email = email;
                _context.SaveChanges();
                var claimsIdentity = _jwt.GetClaimsPrincipal(user);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsIdentity);
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
