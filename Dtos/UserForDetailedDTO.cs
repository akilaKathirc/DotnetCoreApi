using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Dtos
{
    public class UserForDetailedDTO
    {


        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public int Age { get; set; }
        public string password { get; set; }
        public string Gender { get; set; }
        public int Id { get; set; }
        public string KnownAs { get; set; }
        public DateTime LastActive { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<PhotosForDetailsDto> Photos { get; set; }
    }
}
