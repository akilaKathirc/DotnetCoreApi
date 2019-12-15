using Microsoft.AspNetCore.Http;
using System;

namespace WebApplication2.Dtos
{
    public class PhotoForCreationDto
    {
        public IFormFile File { get; set; }

        public string url { get; set; }

        public string PublicID { get; set; }

        public string description { get; set; }

        public DateTime DateAdded { get; set; }

        public int UserId { get; set; }

        public PhotoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}