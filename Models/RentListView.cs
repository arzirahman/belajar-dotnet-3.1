using System.Collections.Generic;

namespace Coba_Net.Models
{
    public class RentListView
    {
        public List<Rent> Rents { set; get; }
        public Pagination Pagination { set; get; }
    }
}