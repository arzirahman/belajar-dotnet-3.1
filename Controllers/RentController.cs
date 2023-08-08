using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using Microsoft.AspNetCore.Authorization;
using Filter;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Coba_Net.Services;

namespace Coba_Net.Controllers
{
    public class RentController : Controller
    {
        private readonly ILogger<RentController> _logger;
        private readonly AppDb _context;
        private readonly RentService _service;

        public RentController(ILogger<RentController> logger, AppDb context, RentService service)
        {
            _logger = logger;
            _context = context;
            _service = service;
        }

        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ValidateCookie))]
        public async Task<IActionResult> Admin(int page = 1, int limit = 5, string search = "")
        {
            var RentListView = await _service.GetRentList(page, limit, search);
            if (TempData.TryGetValue("Message", out var message))
            {
                ViewData["Message"] = message;
            }
            return View("Admin/Admin", RentListView);
        }

        [Authorize(Roles = "admin")]
        [TypeFilter(typeof(ValidateCookie))]
        [Route("/Rent/Admin/Approve")]
        [HttpPost]
        public async Task<IActionResult> AdminApproveRequest(Guid id)
        {
            var rent = await _context.Rents.FindAsync(id);
            if (rent == null){
                return Redirect("/NotFound.html");
            }
            if (rent.GetStatus() == "Pending"){
                rent.ApprovedTime = DateTime.Now;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Rent approved successfully";
            }
            return Redirect("/Rent/Admin");
        }

        [Authorize(Roles = "user")]
        [TypeFilter(typeof(ValidateCookie))]
        public new async Task<IActionResult> User(int page = 1, int limit = 5, string search = "")
        {
            var RentListView = await _service.GetRentList(page, limit, search, Guid.Parse(ViewData["UserId"] as string));
            if (TempData.TryGetValue("Message", out var message))
            {
                ViewData["Message"] = message;
            }
            return View("User/User", RentListView);
        }

        [Authorize(Roles = "user")]
        [TypeFilter(typeof(ValidateCookie))]
        [Route("Rent/User/Request")]
        public async Task<IActionResult> UserRequest()
        {
            List<Car> cars = await _context.Cars.ToListAsync();
            ViewData["Cars"] = cars;
            return View("User/Request");
        }

        [Authorize(Roles = "user")]
        [TypeFilter(typeof(ValidateCookie))]
        [Route("Rent/User/Request")]
        [HttpPost]
        public async Task<IActionResult> UserRequestPost(Rent rent, IFormFile PdfFile)
        {
            List<Car> cars = await _context.Cars.ToListAsync();
            ViewData["Cars"] = cars;
            if (PdfFile == null) ModelState.AddModelError("PdfFile", "Form file is empty");
            else if (Path.GetExtension(PdfFile.FileName).ToLower() != ".pdf")
            {
                ModelState.AddModelError("PdfFile", "Please upload a valid .pdf file.");
            }
            if (ModelState.IsValid)
            {
                await _service.RequestRent(rent, PdfFile, Guid.Parse(ViewData["UserId"] as string));
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
        [Route("/Rent/User/Cancel")]
        [HttpPost]
        public async Task<IActionResult> CancelRequest(Guid id)
        {
            var rent = (ViewData["Role"] as string == "admin") 
            ? await _context.Rents.FirstOrDefaultAsync(r => r.Id == id)
            : await _context.Rents.FirstOrDefaultAsync(r => r.Id == id 
                && r.UserId == Guid.Parse(ViewData["UserId"] as string
            ));
            if (rent == null){
                return Redirect("/NotFound.html");
            }
            if (rent.GetStatus() == "Pending"){
                rent.CancelledTime = DateTime.Now;
                if (ViewData["Role"] as string == "admin") rent.IsCancelledByAdmin = true;
                await _context.SaveChangesAsync();
                TempData["Message"] = "Rent cancelled successfully";
            }
            if (ViewData["Role"] as string == "admin") return Redirect("/Rent/Admin");
            return Redirect("/Rent/User");
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        [Route("/Rent/User/Timeline")]
        public async Task<IActionResult> Timeline(Guid id)
        {
            var rent = await _context.Rents.Include(r => r.Car).FirstOrDefaultAsync(r => r.Id == id);
            if (rent == null)
            {
                return Redirect("/NotFound.html");
            }
            return View("User/Timeline", rent);
        }

        [Authorize]
        [TypeFilter(typeof(ValidateCookie))]
        [HttpGet]
        [Route("/Rent/File/{fileName}")]
        public async Task<IActionResult> GetPdfFile(string fileName)
        {
            if (ViewData["Role"] as string != "admin")
            {
                var rent = await _context.Rents.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(ViewData["UserId"] as string));
                if (rent == null) return Redirect("/NotFound.html");
            }
            if (string.IsNullOrEmpty(fileName))
            {
                return Redirect("/NotFound.html");
            }
            string uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolderPath))
            {
                return Redirect("/NotFound.html");
            }
            string filePath = Path.Combine(uploadsFolderPath, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return Redirect("/NotFound.html");
            }
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            return File(memoryStream.ToArray(), "application/pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
