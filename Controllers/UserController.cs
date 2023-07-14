using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using Coba_Net.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Coba_Net.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDb _context;
        private readonly IJwtService _jwtService;

        public UserController(ILogger<UserController> logger, AppDb context, IJwtService jwtService)
        {
            _logger = logger;
            _context = context;
            _jwtService = jwtService;
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
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Login");
            var token = _jwtService.GenerateToken(claimsIdentity);
            Response.Cookies.Append("session", token, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(15),
                HttpOnly = true,
                Secure = false
            });
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
