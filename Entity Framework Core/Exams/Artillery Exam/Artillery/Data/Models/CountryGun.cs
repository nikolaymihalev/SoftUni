using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery.Data.Models
{
    public class CountryGun
    {
        [Required]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        [Required]
        public int GunId { get; set; }

        [ForeignKey(nameof(GunId))]
        public Gun Gun { get; set; }
    }
}
