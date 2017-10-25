using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Models
{    
    public class Institute
    {
        [Key]
        public int InstituteID { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [Required, StringLength(50)]
        public string City { get; set; }
        
        public int PostalCode { get; set; }

        public int Phone { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
