using System;

namespace Coba_Net.Models
{
    public class Pagination
    {
        public int Page { set; get; }
        public int DataCount { set; get; }
        public int Limit { set; get; }
        public int TotalPages { set; get; }
        public string Search { set; get; }

        public bool HasPrevPage()
        {
            return Page == 1 ? false : true;
        }

        public bool HasNextPage()
        {
            return Page == TotalPages ? false : true;
        }

        public bool IsFirstPage()
        {
            return Page == 1 ? true : false;
        }

        public bool IsLastPage()
        {
            return Page == TotalPages ? true : false;
        }

        public bool IsCenterPage()
        {
            return Page > 1 && Page < TotalPages ? true : false;
        }

        public int GetCenterPage()
        {
            return (int) Math.Floor((double) Page / 2);
        }
    }
}