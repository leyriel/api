using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]     
        public int UserId { get; set; }

        [ForeignKey("EstablishmentID")]
        public int? EstablishmentID { get; set; }

        [Required, StringLength(255)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [Required, StringLength(255)]
        public string Password { get; set; }

        [Required, StringLength(255)]
        public string Skey { get; set; }        

        public virtual Establishments Establishment { get; set; }            
    }
}
