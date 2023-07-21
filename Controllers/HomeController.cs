﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using Microsoft.AspNetCore.Authorization;
using Coba_Net.Utils;

namespace Coba_Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDb _context;
        private readonly Jwt _jwt;

        public HomeController(ILogger<HomeController> logger, AppDb context, Jwt jwt)
        {
            _logger = logger;
            _context = context;
            _jwt = jwt;
        }

        private bool ValidateToken()
        {
            var userInfo = _jwt.ValidateToken(User.Claims.FirstOrDefault(c => c.Type == "Jwt")?.Value);
            if (userInfo == null){
                return false;
            }
            else{
                ViewData["Email"] = userInfo.Email;
                ViewData["Name"] = userInfo.Name;
                ViewData["PpUrl"] = userInfo.PpUrl;
                ViewData["Role"] = userInfo.Role;
                return true;
            }
        }

        [Authorize]
        public IActionResult Index()
        {
            if (!ValidateToken()) return Redirect("/User/Login");
            var query = _context.Cars.AsQueryable();
            var lineChartQuery = query.OrderBy(car => car.CreatedAt);
            var barChartQuery = query.OrderByDescending(car => car.Price);
            var lineData = lineChartQuery.Skip(0).Take(10).ToList();
            var barData = barChartQuery.Skip(0).Take(5).ToList();
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
