using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using Microsoft.AspNetCore.Authorization;
using Filter;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Coba_Net.Controllers
{
    public class RentController : Controller
    {
        private readonly ILogger<RentController> _logger;
        private readonly AppDb _context;

        public RentController(ILogger<RentController> logger, AppDb context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize(Roles = "user")]
        [TypeFilter(typeof(ValidateCookie))]
        public new IActionResult User()
        {
            var rents = _context.Rents
                .Include(r => r.Car)
                .Include(r => r.User)
                .Where(r => r.UserId == Guid.Parse(ViewData["UserId"] as string))
                .ToList();
            if (TempData.TryGetValue("Message", out var message))
            {
                ViewData["Message"] = message;
            }
            return View("User/User", rents);
        }

        [Authorize(Roles = "user")]
        [TypeFilter(typeof(ValidateCookie))]
        [Route("Rent/User/Request")]
        public IActionResult UserRequest()
        {
            List<Car> cars = _context.Cars.ToList();
            ViewData["Cars"] = cars;
            return View("User/Request");
        }

        [Authorize(Roles = "user")]
        [TypeFilter(typeof(ValidateCookie))]
        [Route("Rent/User/Request")]
        [HttpPost]
        public IActionResult UserRequestPost(Rent rent, IFormFile PdfFile)
        {
            List<Car> cars = _context.Cars.ToList();
            ViewData["Cars"] = cars;
            if (PdfFile == null) ModelState.AddModelError("PdfFile", "Form file is empty");
            else if (Path.GetExtension(PdfFile.FileName).ToLower() != ".pdf")
            {
                ModelState.AddModelError("PdfFile", "Please upload a valid .pdf file.");
            }
            if (ModelState.IsValid)
            {
                string uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
                if (!Directory.Exists(uploadsFolderPath))
                {
                    Directory.CreateDirectory(uploadsFolderPath);
                }
                var uuid = Guid.NewGuid();
                string uniqueFileName = uuid.ToString() + Path.GetExtension(PdfFile.FileName);
                string filePath = Path.Combine(uploadsFolderPath, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PdfFile.CopyTo(stream);
                }
                string baseUrl = Request.Scheme + "://" + Request.Host;
                rent.UserId = Guid.Parse(ViewData["UserId"] as string);
                rent.Id = uuid;
                rent.FormUrl = baseUrl + "/Rent/File/" + uniqueFileName;
                _context.Rents.Add(rent);
                _context.SaveChanges();
                TempData["Message"] = "Rent requested successfully";
                return Redirect("/Rent/User");
            }
            else
            {
                return View("User/Request", rent);
            }
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        [HttpGet]
        [Route("/Rent/File/{fileName}")]
        public IActionResult GetPdfFile(string fileName)
        {
            if (ViewData["Role"] as string != "admin")
            {
                var rent = _context.Rents.FirstOrDefault(u => u.UserId == Guid.Parse(ViewData["UserId"] as string));
                if (rent == null) return NotFound();
            }
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound();
            }
            string uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                return NotFound();
            }
            string filePath = Path.Combine(uploadsFolderPath, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            Response.Headers.Add("Content-Disposition", $"inline; filename=\"{fileName}\"");
            return File(fileStream, "application/pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
