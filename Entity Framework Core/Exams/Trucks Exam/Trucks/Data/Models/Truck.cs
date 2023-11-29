using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trucks.Data.Models.Enums;

namespace Trucks.Data.Models
{
    public class Truck
    {
        [Key]
        public int Id { get; set; }
        
        public string RegistrationNumber { get; set; }

        [Required]
        public string VinNumber { get; set; }

        public int TankCapacity { get; set; }
        public int CargoCapacity { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }
        
        [Required]
        public MakeType MakeType { get; set; }

        [Required]
        public int DespatcherId { get; set; }

        [ForeignKey(nameof(DespatcherId))]
        public Despatcher Despatcher { get; set; }

        public ICollection<ClientTruck> ClientsTrucks { get; set; } = new List<ClientTruck>();
    }
}
