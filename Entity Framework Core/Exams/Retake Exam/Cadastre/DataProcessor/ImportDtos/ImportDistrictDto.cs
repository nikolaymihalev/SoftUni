using Cadastre.Data.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cadastre.DataProcessor.ImportDtos
{
    [XmlType("District")]
    public class ImportDistrictDto
    {
        [Required]
        [MaxLength(80)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{2}-\d{5}$")]
        public string PostalCode { get; set; }
        
        [XmlAttribute("Region")]
        [EnumDataType(typeof(Region))]
        public Region Region { get; set; }

        [XmlArray("Properties")]
        public ImportPropertyDto[] Properties { get; set; }
    }
}
