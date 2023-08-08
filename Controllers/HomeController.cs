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
using Coba_Net.Services;

namespace Coba_Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarService _carService;
        private readonly HomeService _homeService;

        public HomeController(ILogger<HomeController> logger, CarService carService, HomeService homeService)
        {
            _logger = logger;
            _carService = carService;
            _homeService = homeService; 
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        public async Task<IActionResult> Index(int page = 1, int limit = 6, string search = "")
        {
            var CarListView = await _carService.GetCarList(page, limit, search);
            return View(CarListView);
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        public async Task<IActionResult> Chart()
        {
            var chart = await _homeService.Chart();
            return View("Chart", chart);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
