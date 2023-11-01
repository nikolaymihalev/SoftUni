using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_HospitalDatabase.Data.Models
{
    public class Medicament
    {
        public int MedicamentId { get; set; }
        public string Name { get; set; }
        public ICollection<PatientMedicament> Prescriptions { get; set; }
    }
}
