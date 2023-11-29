using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Trucks.DataProcessor.ImportDto
{
    [XmlType("Despatcher")]
    public class ImportDespatchersDTO
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        public string Name { get; set; }
        public string Position { get; set; }

        public ImportTruckDTO[] Trucks { get; set; }
    }
}
