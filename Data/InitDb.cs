using Coba_Net.Models;
using System.Linq;
using System;

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
                    new User { Email = "admin@example.com", Name = "Admin", Password = "admin", Role = "admin" },
                    new User { Email = "user@example.com", Name = "User", Password = "user", Role = "user" }
                };
                foreach (var user in users)
                {
                    user.HashPassword();
                }
                _context.User.AddRange(users);
                _context.SaveChanges();
            }
            if (!_context.Cars.Any())
            {
                var cars = new Car[]
                {
                    new Car { Name = "Kijang Innova", Brand = "Toyota", Color = "Black", Price = 370000000, CreatedAt = DateTime.Now },
                    new Car { Name = "Honda HRV", Brand = "Honda", Color = "Red", Price = 376000000, CreatedAt = DateTime.Now },
                    new Car { Name = "Mitsubishi Xpander", Brand = "Mitsubishi", Color = "Silver", Price = 258000000, CreatedAt = DateTime.Now },
                    new Car { Name = "Honda Brio", Brand = "Honda", Color = "Yellow", Price = 166000000, CreatedAt = DateTime.Now },
                    new Car { Name = "Hyundai Stargazer", Brand = "Hyundai", Color = "White", Price = 220000000, CreatedAt = DateTime.Now },
                    new Car { Name = "Nissan Kicks 2023", Brand = "Nissan", Color = "Orange", Price = 277000000, CreatedAt = DateTime.Now },
                    new Car { Name = "Suzuki Vitara Brezza", Brand = "Suzuki", Color = "Red", Price = 165000000, CreatedAt = DateTime.Now },
                };
                _context.Cars.AddRange(cars);
                _context.SaveChanges();
            }
        }
    }
}
