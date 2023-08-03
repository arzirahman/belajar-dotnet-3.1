using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using Microsoft.AspNetCore.Authorization;
using Filter;
using Microsoft.EntityFrameworkCore;

namespace Coba_Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDb _context;

        public HomeController(ILogger<HomeController> logger, AppDb context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        public async Task<IActionResult> Index(int page = 1, int limit = 6, string search = "")
        {
            page = page <= 0 ? 1 : page;
            limit = limit <= 0 ? 6 : limit;
            var query = _context.Cars.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(car =>
                    car.Name.Contains(search) ||
                    car.Brand.Contains(search) ||
                    car.Color.Contains(search) ||
                    car.Price.ToString().Contains(search)
                );
            }
            query = query.OrderByDescending(car => car.CreatedAt);
            var totalCars = query.Count();
            var totalPages = (int) Math.Ceiling((double) totalCars / limit);
            var cars = await query.Skip((page - 1) * limit).Take(limit).ToListAsync();
            var Pagination = new Pagination
            {
                Page = page,
                Limit = limit,
                TotalPages = totalPages,
                DataCount = totalCars,
                Search = search
            };
            var CarListView = new CarListView
            {
                Pagination = Pagination,
                Cars = cars
            };
            return View(CarListView);
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        public async Task<IActionResult> Chart()
        {
            var query = _context.Cars.AsQueryable();
            var lineChartQuery = query.OrderBy(car => car.CreatedAt);
            var barChartQuery = query.OrderByDescending(car => car.Price);
            var lineData = await lineChartQuery.Skip(0).Take(10).ToListAsync();
            var barData = await barChartQuery.Skip(0).Take(5).ToListAsync();
            var chart = new Chart{
                LineData = lineData,
                BarData = barData
            };
            return View("Chart", chart);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
