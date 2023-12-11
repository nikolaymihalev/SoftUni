using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("Property")]
    public class ImportPropertyDto
    {
        [Required]
        [MaxLength(20)]
        [MinLength(16)]
        public string PropertyIdentifier { get; set; }

        [Required]
        [Range(0, 2147483647)]
        public int Area { get; set; }

        [MaxLength(500)]
        [MinLength(5)]
        public string Details { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(5)]
        public string Address { get; set; }

        [Required]
        public string DateOfAcquisition { get; set; }
    }
}
