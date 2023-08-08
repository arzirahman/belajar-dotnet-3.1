using Coba_Net.Data;
using Coba_Net.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Coba_Net.Services
{
    public class HomeService
    {
        private readonly AppDb _dbContext;

        public HomeService(AppDb dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Chart> Chart()
        {
            var query = _dbContext.Cars.AsQueryable();
            var lineChartQuery = query.OrderBy(car => car.CreatedAt);
            var barChartQuery = query.OrderByDescending(car => car.Price);
            var lineData = await lineChartQuery.Skip(0).Take(10).ToListAsync();
            var barData = await barChartQuery.Skip(0).Take(5).ToListAsync();
            var chart = new Chart{
                LineData = lineData,
                BarData = barData
            };
            return chart;
        }
    }
}