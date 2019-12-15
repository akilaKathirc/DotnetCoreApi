using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Helpers
{
    public class PaginationHelper
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }
        public int TotalPage { get; set; }

        public PaginationHelper(int CurrentPage, int ItemsPerPage, int TotalItems, int TotalPage)
        {
            this.CurrentPage = CurrentPage;
            this.ItemsPerPage = ItemsPerPage;
            this.TotalItems = TotalItems;
            this.TotalPage = TotalPage;

        }
    }
}
