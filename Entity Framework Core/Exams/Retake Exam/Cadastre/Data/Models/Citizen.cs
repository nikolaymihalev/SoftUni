using Cadastre.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cadastre.Data.Models
{
    public class Citizen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        public ICollection<PropertyCitizen> PropertiesCitizens { get; set; } = new List<PropertyCitizen>();
    }
}
