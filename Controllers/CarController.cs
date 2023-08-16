using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using System.Linq;
using System;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Filter;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Coba_Net.Services;

namespace Coba_Net.Controllers
{
    [Authorize(Roles = "admin")]
    [TypeFilter(typeof(ValidateCookie))]
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDb _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CarService _service;

        public CarController(IWebHostEnvironment webHostEnvironment, ILogger<CarController> logger, AppDb context, CarService service)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _service = service;
        }
        
        public async Task<IActionResult> Index(
            int page = 1, int limit = 5, string search = "", string sortBy = "CreatedAt", string sortOrder = "desc",
            string Brand = "", string Color = "", string FromPrice = "", string ToPrice = ""
        )
        {
            ViewData["CurrentPath"] = Request.Path;
            ViewData["QueryString"] = Request.QueryString.ToString();
            var CarListView = await _service.GetCarList(page, limit, search, sortBy, sortOrder, Brand, Color, FromPrice, ToPrice);
            if (TempData.TryGetValue("ModelStateError", out var errorMessageObject) && errorMessageObject is Dictionary<string, string> errors)
            {
                foreach (var (key, errorMessage) in errors)
                {
                    ModelState.AddModelError(key, errorMessage);
                }
            }
            if (TempData.TryGetValue("Message", out var message))
            {
                ViewData["Message"] = message;
            }
            return View(CarListView);
        }

        
        [HttpGet]
        public IActionResult Add()
        {
            var car = new Car();
            return View(car);
        }

        private void FileValidation(IFormFile file)
        {
            if (file != null && file.Length > 0){
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (file.Length > 0 && file.Length > 500 * 1024)
                {
                    ModelState.AddModelError("file", "Image size cannot exceed 2 MB.");
                }
                else if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("file", "Only .jpg, .jpeg, and .png files are allowed.");
                }
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Car car, IFormFile file)
        {
            FileValidation(file);
            if (ModelState.IsValid)
            {
                await _service.Add(file, car);
                TempData["Message"] = "Car added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(car);
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return Redirect("/NotFound.html");
            }
            await _service.Delete(car);
            TempData["Message"] = "Car deleted successfully";
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var car = await _context.Cars.FindAsync(Id);
            if (car == null)
            {
                return Redirect("/NotFound.html");
            }
            return View("Add", car);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(Car car, IFormFile file)
        {
            FileValidation(file);
            if (ModelState.IsValid)
            {
                var existingCar = await _context.Cars.FindAsync(car.Id);
                if (existingCar == null)
                {
                    return Redirect("/NotFound.html");
                }
                await _service.Edit(file, existingCar, car);
                TempData["Message"] = "Car edited successfully";
                return RedirectToAction("Index");
            }
            return View("Add", car);
        }

        
        [HttpGet]
        public async Task<IActionResult> Download()
        {
            var stream = await _service.Download();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CarList.xlsx");
        }

        
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                ModelState.AddModelError("file", "Please select a valid file.");
            }
            else if (Path.GetExtension(file.FileName).ToLower() != ".xlsx")
            {
                ModelState.AddModelError("file", "Please upload a valid .xlsx file.");
            }
            if (ModelState.IsValid)
            {
                await _service.Upload(file);
                TempData["Message"] = "File uploaded successfully";
                return RedirectToAction("Index");
            }
            var errors = new Dictionary<string, string>();
            foreach (var keyModelStatePair in ModelState)
            {
                var key = keyModelStatePair.Key;
                var error = keyModelStatePair.Value.Errors.FirstOrDefault()?.ErrorMessage;
                errors[key] = error;
            }
            TempData["ModelStateError"] = errors;
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
