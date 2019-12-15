using System;

namespace WebApplication2.Dtos
{
    public class PhotoToReturnDTO
    {
        public int ID { get; set; }

        public string url { get; set; }
        public bool isMain { get; set; }

        public string description { get; set; }

        public DateTime DateAdded { get; set; }
        public string PublicID { get; set; }


    }
}