using System.Collections.Generic;

namespace Coba_Net.Models
{
    public class CarListView
    {
        public List<Car> Cars { set; get; }
        public Pagination Pagination { set; get; }
        public Car SelectedCar { set; get; }
    }
}