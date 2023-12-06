﻿using Artillery.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery.Data.Models
{
    public class Gun
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        public int GunWeight { get; set; }

        [Required]
        public double BarrelLength { get; set; }

        public int NumberBuild { get; set; }

        [Required]
        public int Range { get; set; }

        [Required]
        public GunType GunType { get; set; }

        [Required]
        public int ShellId { get; set; }

        [ForeignKey(nameof(ManufacturerId))]
        public Manufacturer Manufacturer { get; set; }

        [ForeignKey(nameof(ShellId))]
        public Shell Shell { get; set; }

        public ICollection<CountryGun> CountriesGuns { get; set; } = new List<CountryGun>();
    }
}
