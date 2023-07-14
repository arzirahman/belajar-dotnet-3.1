using Coba_Net.Models;
using System.Linq;

namespace Coba_Net.Data
{
    public class InitDb
    {
        private readonly AppDb _context;

        public InitDb(AppDb context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();
            if (!_context.User.Any())
            {
                var users = new User[]
                {
                    new User { Email = "user1@example.com", Name = "User 1", Password = "password1" },
                    new User { Email = "user2@example.com", Name = "User 2", Password = "password2" }
                };
                foreach (var user in users)
                {
                    user.HashPassword();
                }
                _context.User.AddRange(users);
                _context.SaveChanges();
            }
        }
    }
}
