﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery.Data.Models
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ManufacturerName { get; set; } 
        
        [Required]
        public string Founded { get; set; }

        public ICollection<Gun> Guns { get; set; } = new List<Gun>();
    }
}
