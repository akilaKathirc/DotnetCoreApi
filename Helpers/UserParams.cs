using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Helpers
{
    public class UserParams
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int PageSize = 10;

        public int MyProperty
        {
            get { return PageSize ; }
            set { PageSize  = (value > MaxPageSize) ? MaxPageSize:value; }
        }

    }
}
