using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("Establishments")]
    public class Establishments
    {   
        [Key]
        public int EstablishmentID { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, StringLength(255)]
        public string Address{ get; set; }

        [Required, StringLength(255)]
        public string City { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "The postal code must be have 5 number"), MinLength(5, ErrorMessage = "The postal code must be have 5 number")]
        public int PostalCode { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
