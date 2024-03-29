﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Dtos
{
    public class UserToRegisterDto
    {
        [Required]
        public string userName { get; set; }

        [Required]
        [StringLength(8, MinimumLength =4, ErrorMessage ="Password length should be between 4 to 8 characters")]
        public string passWord { get; set; }

        [Required]

        public string Gender { get; set; }

        [Required]

        public string KnownAs { get; set; }

        [Required]

        public string City { get; set; }

        [Required]

        public DateTime DateOfBirth { get; set; }

        [Required]

        public string Country { get; set; }


        public DateTime Created { get; set; }


        public DateTime LastActive { get; set; }

        public UserToRegisterDto()
        {
                Created = DateTime.Now;
                LastActive = DateTime.Now;
        }
    }
}
