using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Coba_Net.Models;
using Coba_Net.Data;
using System.Linq;
using System;
using OfficeOpenXml;
using System.IO;

namespace Coba_Net.Controllers
{
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;
        private readonly AppDb _context;

        public CarController(ILogger<CarController> logger, AppDb context)
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

        public IActionResult Index(int page = 1, int limit = 5, string search = "")
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
            ViewDataInit();
            return View(CarListView);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var car = new Car();
            ViewDataInit();
            return View(car);
        }

        [HttpPost]
        public IActionResult Add(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(car);
            }
        }

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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid Id)
        {
            var car = _context.Cars.Find(Id);
            if (car == null)
            {
                return NotFound();
            }
            ViewDataInit();
            return View("Add", car);
        }

        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                Car existingCar = _context.Cars.Find(car.Id);
                if (existingCar == null)
                {
                    return NotFound();
                }
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewDataInit();
            return View("Add", car);
        }

        [HttpGet]
        public IActionResult Download()
        {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
