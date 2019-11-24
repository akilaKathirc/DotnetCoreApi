using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Dtos
{
    public class PhotosForDetailsDto
    {
        public int ID { get; set; }

        public string url { get; set; }
        public bool isMain { get; set; }

        public string description { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
