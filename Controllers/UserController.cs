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
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Coba_Net.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDb _context;
        private readonly Jwt _jwt;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IWebHostEnvironment webHostEnvironment, ILogger<UserController> logger, AppDb context, IConfiguration configuration, Jwt jwt)
        {
            _logger = logger;
            _context = context;
            _jwt = jwt;
            _webHostEnvironment = webHostEnvironment;
        }

        private void ViewDataInit()
        {
            ViewData["Email"] = HttpContext.Items["Email"];
            ViewData["Name"] = HttpContext.Items["Name"];
            ViewData["PpUrl"] = HttpContext.Items["PpUrl"];
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new Login());
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == login.Email);
            if (user == null || !user.VerifyPassword(login.Password))
            {
                var error = new Login{
                    ErrorMessage = "Wrong username or password"
                };
                return View(error);
            }
            var token = _jwt.GenerateToken(user);
            Response.Cookies.Append("session", token, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                HttpOnly = true,
                Secure = false
            });
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("session");
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var user = _context.User.FirstOrDefault(user => user.Email == (string) HttpContext.Items["Email"]);
            ViewDataInit();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(IFormFile file, string name, string email)
        {
            var user = _context.User.FirstOrDefault(user => user.Email == (string) HttpContext.Items["Email"]);
            var formatter = new Formatter();
            if (file != null && file.Length > 0 && file.Length >  500 * 1024)
            {
                ModelState.AddModelError("file", "Image size cannot exceed 500 KB.");
            }
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("name", "Name is empty.");
            }
            else if (name.Length > 20)
            {
                ModelState.AddModelError("name", "Name cannot exceed 20 character.");   
            }
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("email", "Email is empty."); 
            } 
            else if (!formatter.IsEmailValid(email))
            {
                ModelState.AddModelError("email", "Invalid format.");    
            }
            else if (email != user.Email)
            {
                var existingUser = _context.User.FirstOrDefault(user => user.Email == email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("email", "This email is already in use.");
                }
            }
            if (ModelState.IsValid)
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
                var newToken = _jwt.GenerateToken(user);
                Response.Cookies.Append("session", newToken, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                    HttpOnly = true,
                    Secure = false
                });
                return RedirectToAction("Profile", "User");
            }
            else{
                ViewDataInit();
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
