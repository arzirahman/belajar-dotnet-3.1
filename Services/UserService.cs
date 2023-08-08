using System.Threading.Tasks;
using Coba_Net.Models;
using Coba_Net.Data;
using Coba_Net.Utils;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Coba_Net.Services
{
    public class UserService
    {
        private readonly AppDb _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Jwt _jwt;

        public UserService(IWebHostEnvironment webHostEnvironment, AppDb dbContext, IHttpContextAccessor httpContext, Jwt jwt)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
            _webHostEnvironment = webHostEnvironment;
            _jwt = jwt;
        }

        public async Task Profile(User user, IFormFile file, string name, string email)
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
            await _dbContext.SaveChangesAsync();
            var claimsIdentity = _jwt.GetClaimsPrincipal(user);
            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsIdentity);
        }
    }
}