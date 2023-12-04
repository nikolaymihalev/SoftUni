using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Patient")]
    public class ExportPatientDTO
    {
        public string Name { get; set; }
        public string AgeGroup { get; set; }

        [XmlAttribute()]
        public string Gender { get; set; }

        [XmlArray("Medicines")]
        public ExportMedicineDTO[] Medicines { get; set; } = new ExportMedicineDTO[0];
    }
}
