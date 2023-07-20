using System.Linq;
using Coba_Net.Data;
using Coba_Net.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


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

        public bool ValidateProfile(User user, IFormFile file, string name, string email, out Dictionary<string, string> errors)
        {
            errors = new Dictionary<string, string>();
            if (file != null && file.Length > 0 && file.Length > 500 * 1024)
            {
                errors["file"] = "Image size cannot exceed 500 KB.";
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
                var existingUser = _context.User.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    errors["Email"] = "This email is already in use.";
                }
            }
            if (errors != null && errors.Count > 0){
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}