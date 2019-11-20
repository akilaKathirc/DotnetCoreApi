using System;
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

    }
}
