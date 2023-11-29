﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trucks.DataProcessor.ImportDto
{
    public class ImportTruckDTO
    {
        [StringLength(8)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(17)]
        public string VinNumber { get; set; } = null!;

        [Range(950,1420)]
        public int TankCapacity { get; set; }

        [Range(5000, 29000)]
        public int CargoCapacity { get; set; }

        [Required]
        [Range(0,3)]
        public int CategoryType { get; set; }

        [Required]
        [Range(0, 4)]
        public int MakeType { get; set; }
    }
}
