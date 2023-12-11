using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.Data.Models
{
    public class PropertyCitizen
    {
        [Required]
        public int PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public Property Property { get; set; }

        [Required]
        public int CitizenId { get; set; }

        [ForeignKey(nameof(CitizenId))]
        public Citizen Citizen { get; set; }
    }
}
