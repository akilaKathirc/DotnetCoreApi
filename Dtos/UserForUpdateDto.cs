using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Dtos
{
    public class UserForUpdateDto
    {
        public string Interest { get; set; }
        public string Introduction { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string LookingFor { get; set; }
    }
}
