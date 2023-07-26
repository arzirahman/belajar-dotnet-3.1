using System.Linq;
using Coba_Net.Data;
using Coba_Net.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Coba_Net.Utils
{
    public class ProfileValidator
    {
        private readonly AppDb _context;
        private readonly Formatter _formatter;

        public ProfileValidator(AppDb context)
        {
            _context = context;
            _formatter = new Formatter();
        }

        public async Task<(bool IsValid, Dictionary<string, string> Errors)> ValidateProfile(User user, IFormFile file, string name, string email)
        {
            var errors = new Dictionary<string, string>();
            if (file != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (file.Length > 0 && file.Length > 500 * 1024)
                {
                    errors["file"] = "Image size cannot exceed 500 KB.";
                }
                else if (!allowedExtensions.Contains(fileExtension))
                {
                    errors["file"] = "Only .jpg, .jpeg, and .png files are allowed.";
                }
            }
            if (string.IsNullOrEmpty(name))
            {
                errors["Name"] = "Name is empty.";
            }
            else if (name.Length > 20)
            {
                errors["Name"] = "Name cannot exceed 20 characters.";
            }
            if (string.IsNullOrEmpty(email))
            {
                errors["Email"] = "Email is empty.";
            }
            else if (!_formatter.IsEmailValid(email))
            {
                errors["Email"] = "Invalid email format.";
            }
            else if (email != user.Email)
            {
                var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
                if (existingUser != null)
                {
                    errors["Email"] = "This email is already in use.";
                }
            }
            if (errors != null && errors.Count > 0){
                return (false, errors);
            }
            else
            {
                return (true, null);
            }
        }
    }
}