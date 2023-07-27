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

namespace Coba_Net.Controllers
{
    [Authorize(Roles = "admin")]
    [TypeFilter(typeof(ValidateCookie))]
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDb _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarController(IWebHostEnvironment webHostEnvironment, ILogger<CarController> logger, AppDb context)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public async Task<IActionResult> Index(int page = 1, int limit = 5, string search = "")
        {
            page = page <= 0 ? 1 : page;
            limit = limit <= 0 ? 5 : limit;
            var query = _context.Cars.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(car =>
                    car.Name.Contains(search) ||
                    car.Brand.Contains(search) ||
                    car.Color.Contains(search)
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

        private async Task<string> UploadFile(IFormFile file, string fileName)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "car");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            string filePath = Path.Combine(uploadsFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return "/car/" + fileName;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(Car car, IFormFile file)
        {
            FileValidation(file);
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var uuid = Guid.NewGuid();
                    string extension = Path.GetExtension(file.FileName);
                    string fileName = uuid.ToString() + extension;
                    var url = await UploadFile(file, fileName);
                    car.Id = uuid;
                    car.PicUrl = "/car/" + fileName;
                }
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();
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
                return NotFound();
            }
            if (!string.IsNullOrEmpty(car.PicUrl))
            {
                var fileName = Path.GetFileName(car.PicUrl);
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "car", fileName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Car deleted successfully";
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var car = await _context.Cars.FindAsync(Id);
            if (car == null)
            {
                return NotFound();
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
                    return NotFound();
                }
                if (file != null && file.Length > 0)
                {
                    var fileName = (existingCar.PicUrl == null)
                    ? car.Id.ToString() + Path.GetExtension(file.FileName)
                    : Path.GetFileName(existingCar.PicUrl);
                    var url = await UploadFile(file, fileName);
                    car.PicUrl = existingCar.PicUrl ?? url;
                }
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Car edited successfully";
                return RedirectToAction("Index");
            }
            return View("Add", car);
        }

        
        [HttpGet]
        public async Task<IActionResult> Download()
        {
            var cars = await _context.Cars.ToListAsync();
            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Car List");
                var headers = new[] { "Name", "Brand", "Color", "Price" };
                for (int i = 0; i < headers.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];   
                }
                for (int i = 0; i < cars.Count; i++)
                {
                    var car = cars[i];
                    worksheet.Cells[i + 2, 1].Value = car.Name;
                    worksheet.Cells[i + 2, 2].Value = car.Brand;
                    worksheet.Cells[i + 2, 3].Value = car.Color;
                    worksheet.Cells[i + 2, 4].Value = car.Price;
                }
                var stream = new MemoryStream();
                excelPackage.SaveAs(stream);
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CarList.xlsx");
            }
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
                using (var package = new ExcelPackage(file.OpenReadStream()))
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        package.Load(stream);
                    }
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    if (worksheet.Dimension != null)
                    {
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            string name = worksheet.Cells[row, 1].Value?.ToString();
                            string brand = worksheet.Cells[row, 2].Value?.ToString();
                            string color = worksheet.Cells[row, 3].Value?.ToString();
                            decimal price = decimal.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out decimal parsedPrice) ? parsedPrice : 0;
                            if (name != null && brand != null && color != null)
                            {
                                var car = new Car
                                {
                                    Name = name,
                                    Brand = brand,
                                    Color = color,
                                    Price = price
                                };
                                _context.Add(car);
                            }
                        }
                        await _context.SaveChangesAsync();
                    }
                }
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
