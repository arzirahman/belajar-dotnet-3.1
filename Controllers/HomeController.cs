using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;

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

        private void ViewDataInit()
        {
            ViewData["Email"] = HttpContext.Items["Email"];
            ViewData["Name"] = HttpContext.Items["Name"];
            ViewData["PpUrl"] = HttpContext.Items["PpUrl"];
        }

        public IActionResult Index()
        {
            var query = _context.Cars.AsQueryable();
            var lineChartQuery = query.OrderBy(car => car.CreatedAt);
            var barChartQuery = query.OrderBy(car => car.Price);
            var lineData = lineChartQuery.Skip(0).Take(10).ToList();
            var barData = barChartQuery.Skip(0).Take(5).ToList();
            ViewDataInit();
            var chart = new Chart{
                LineData = lineData,
                BarData = barData
            };
            return View(chart);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
