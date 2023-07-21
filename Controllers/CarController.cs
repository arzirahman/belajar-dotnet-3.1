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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Coba_Net.Utils;

namespace Coba_Net.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDb _context;
        private readonly Jwt _jwt;

        public CarController(ILogger<CarController> logger, AppDb context, Jwt jwt)
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

        [Authorize(Roles = "admin")]
        public IActionResult Index(int page = 1, int limit = 5, string search = "")
        {
            if (!ValidateToken()) return Redirect("/User/Login");
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
            var cars = query.Skip((page - 1) * limit).Take(limit).ToList();
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

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Add()
        {
            if (!ValidateToken()) return Redirect("/User/Login");
            var car = new Car();
            return View(car);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Add(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                TempData["Message"] = "Car added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(car);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            var car = _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }
            _context.Cars.Remove(car);
            _context.SaveChanges();
            TempData["Message"] = "Car deleted successfully";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            if (!ValidateToken()) return Redirect("/User/Login");
            var car = _context.Cars.Find(Id);
            if (car == null)
            {
                return NotFound();
            }
            return View("Add", car);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (!ValidateToken()) return Redirect("/User/Login");
            if (ModelState.IsValid)
            {
                Car existingCar = _context.Cars.Find(car.Id);
                if (existingCar == null)
                {
                    return NotFound();
                }
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                _context.SaveChanges();
                TempData["Message"] = "Car edited successfully";
                return RedirectToAction("Index");
            }
            return View("Add", car);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult Download()
        {
            if (!ValidateToken()) return Redirect("/User/Login");
            var cars = _context.Cars.ToList();
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

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (!ValidateToken()) return Redirect("/User/Login");
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
                await Task.Run(() =>
                {
                    using (var package = new ExcelPackage(file.OpenReadStream()))
                    {
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
                                float price = float.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out float parsedPrice) ? parsedPrice : 0;
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
                            _context.SaveChanges();
                        }
                    }
                });
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
