using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmacyDTO
    {
        [MaxLength(50)]
        [MinLength(2)]
        [Required]
        public string Name { get; set; }

        [StringLength(14)]
        [Required]
        [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [XmlAttribute("non-stop")]
        [RegularExpression(@"^(true|false)$")]
        public string IsNonStop { get; set; }

        [XmlArray()]
        public ImportPharmacyMedicineDTO[] Medicines { get; set; }
    }
}
