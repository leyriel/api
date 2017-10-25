using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class User
    {
        [Key]     
        public int UserID { get; set; }

        [Required, StringLength(255)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(255)]
        public string Password { get; set; }

        public string Skey { get; set; }       
        
        public int? InstituteID { get; set; }
    }
}
