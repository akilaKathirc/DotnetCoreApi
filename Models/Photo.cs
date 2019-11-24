using System;

namespace WebApplication2.Models
{
    public class Photos
    {
        public int ID { get; set; }

        public string url { get; set; }
        public bool isMain { get; set; }

        public string description { get; set; }

        public DateTime DateAdded { get; set; }

        public Users user { get; set; }

        public int UserId { get; set; }

    }
}