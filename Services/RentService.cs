using Coba_Net.Data;
using System.Threading.Tasks;
using Coba_Net.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Filter;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using Coba_Net.Services;

namespace Coba_Net.Services
{
    public class RentService
    {
        private readonly AppDb _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public RentService(AppDb dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        public async Task<RentListView> GetRentList(int page = 1, int limit = 5, string search = "", Guid? userId = null)
        {
            page = page <= 0 ? 1 : page;
            limit = limit <= 0 ? 5 : limit;
            var query = _dbContext.Rents.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Include(r => r.Car).Include(r => r.User).Where(r =>
                    r.Id.ToString().Contains(search) ||
                    r.User.Email.Contains(search) ||
                    r.User.Name.Contains(search) ||
                    r.Car.Name.Contains(search)
                );
            }
            else
            {
                query = query.Include(r => r.Car).Include(r => r.User);
            }
            query = query.OrderByDescending(r => r.CreatedAt);
            if (userId != null){
                query = query.Where(r => r.User.Id == userId);
            }
            var totalRent = await query.CountAsync();
            var totalPages = (int) Math.Ceiling((double) totalRent / limit);
            var rents = await query.Skip((page - 1) * limit).Take(limit).ToListAsync();
            var Pagination = new Pagination
            {
                Page = page,
                Limit = limit,
                TotalPages = totalPages,
                DataCount = totalRent,
                Search = search
            };
            var RentListView = new RentListView
            {
                Pagination = Pagination,
                Rents = rents
            };
            return RentListView;
        }

        public async Task RequestRent(Rent rent, IFormFile PdfFile, Guid userId)
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
                await PdfFile.CopyToAsync(stream);
            }
            rent.UserId = userId;
            rent.Id = uuid;
            rent.FormUrl = "/Rent/File/" + uniqueFileName;
            await _dbContext.Rents.AddAsync(rent);
            await _dbContext.SaveChangesAsync();
        }
    }
}