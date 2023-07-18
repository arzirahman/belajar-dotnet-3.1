using System.Collections.Generic;

namespace Coba_Net.Models
{
    public class TableProps
    {
        public string[] Headers { set; get; } 
        public string[] Keys { set; get; } 
        public List<Car> List { set; get; } 
        public string UpdatePath { set; get; } 
        public string DeletePath { set; get; } 
    }
}