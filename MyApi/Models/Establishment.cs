using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Establishments
    {   
        [key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address{ get; set; }
        public string City { get; set; }
        
        [MaxLength(5, ErrorMessage = "The postal code must be have 5 number"), MinLength(5, ErrorMessage = "The postal code must be have 5 number")]
        public int PostalCode { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
