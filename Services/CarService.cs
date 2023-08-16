using Coba_Net.Data;
using Coba_Net.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;

namespace Coba_Net.Services
{
    public class CarService
    {
        private readonly AppDb _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CarService(IWebHostEnvironment webHostEnvironment, AppDb dbContext)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<CarListView> GetCarList(
            int page = 1, int limit = 5, string search = "", string sortBy = "CreatedAt", string sortOrder = "desc",
            string brand = "", string color = "", string fromPrice = "", string toPrice = ""
        )
        {
            page = page <= 0 ? 1 : page;
            limit = limit <= 0 ? 5 : limit;
            var query = _dbContext.Cars.AsQueryable();
            var carList = await _dbContext.Cars.ToListAsync();
            var brandList = carList.Select(c => c.Brand).Distinct().ToList();
            var colorList = carList.Select(c => c.Color).Distinct().ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(car =>
                    car.Name.Contains(search) ||
                    car.Brand.Contains(search) ||
                    car.Color.Contains(search)
                );
            }
            if (!string.IsNullOrEmpty(brand)) {
                query = query.Where(car => car.Brand == brand);
            }
            if (!string.IsNullOrEmpty(color)) {
                query = query.Where(car => car.Color == color);
            }
            if (decimal.TryParse(fromPrice, out decimal priceFromValue)) {
                query = query.Where(car => car.Price >= priceFromValue);
            }
            if (decimal.TryParse(toPrice, out decimal priceToValue)) {
                query = query.Where(car => car.Price <= priceToValue);
            }
            if (sortBy == "Name")
            {
                if (sortOrder == "desc") query = query.OrderByDescending(car => car.Name);
                else query = query.OrderBy(car => car.Name);
            }
            else if (sortBy == "Price")
            {
                if (sortOrder == "asc") query = query.OrderBy(car => car.Price);
                else query = query.OrderByDescending(car => car.Price);
            }
            else
            {
                if (sortOrder == "asc") query = query.OrderBy(car => car.CreatedAt);
                else query = query.OrderByDescending(car => car.CreatedAt);
            }
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
            var Sort = new Sort
            {
                SortBy = sortBy,
                SortOrder = sortOrder
            };

            var CarListView = new CarListView
            {
                Pagination = Pagination,
                Cars = cars,
                Sort = Sort,
                BrandFilter = brandList,
                ColorFilter = colorList
            };
            return CarListView;
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

        public async Task Add(IFormFile file, Car car)
        {
            if (file != null && file.Length > 0)
            {
                var uuid = Guid.NewGuid();
                string extension = Path.GetExtension(file.FileName);
                string fileName = uuid.ToString() + extension;
                var url = await UploadFile(file, fileName);
                car.Id = uuid;
                car.PicUrl = url;
            }
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Car car)
        {
            if (!string.IsNullOrEmpty(car.PicUrl))
            {
                var fileName = Path.GetFileName(car.PicUrl);
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "car", fileName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(IFormFile file, Car existingCar, Car car)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = (existingCar.PicUrl == null)
                ? car.Id.ToString() + Path.GetExtension(file.FileName)
                : Path.GetFileName(existingCar.PicUrl);
                var url = await UploadFile(file, fileName);
                car.PicUrl = existingCar.PicUrl ?? url;
            }
            var createdAt = existingCar.CreatedAt;
            _dbContext.Entry(existingCar).CurrentValues.SetValues(car);
            existingCar.CreatedAt = createdAt;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MemoryStream> Download()
        {
            var cars = await _dbContext.Cars.ToListAsync();
            using var excelPackage = new ExcelPackage();
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
            return stream;
        }

        public async Task Upload(IFormFile file)
        {
            using var package = new ExcelPackage(file.OpenReadStream());
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                package.Load(stream);
            }
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            if (worksheet.Dimension != null)
            {
                int rowCount = worksheet.Dimension.Rows;
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
                        _dbContext.Add(car);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}