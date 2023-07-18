using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Html;

namespace Coba_Net.Models
{
    public class TableProps
    {
        public string[] Headers { set; get; } 
        public string[] Keys { set; get; } 
        public List<Car> List { set; get; } 
        public string UpdatePath { set; get; } 
        public string DeletePath { set; get; }
        public Func<string, Dictionary<string, object>, IHtmlContent> BodyFormatter { set; get; }
    }
}