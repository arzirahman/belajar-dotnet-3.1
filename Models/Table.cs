using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;

namespace Coba_Net.Models
{
    public class Header
    {
        public string Label { set; get; }
        public bool EnableSort { set; get; } = false;
        public string Key { set; get; }
    }
    
    public class Sort
    {
        public string SortBy { set; get; }
        public string SortOrder { set; get; }
    }

    public class SortProps
    {
        public Sort Current { set; get; }
        public string Key { set; get; }
        public string Url { set; get; }
    }

    public class TableProps
    {
        public Header[] Headers { set; get; }
        public List<Car> List { set; get; } 
        public string UpdatePath { set; get; } 
        public string DeletePath { set; get; }
        public Func<string, Dictionary<string, object>, IHtmlContent> BodyFormatter { set; get; }
        public Sort Sort { set; get; }
        public string SortPath { set; get; }
    }

    public class TableActionProps
    {
        public Guid IdData { set; get; }
        public string UpdatePath { set; get; }
        public string DeletePath { set; get; }
    }
}