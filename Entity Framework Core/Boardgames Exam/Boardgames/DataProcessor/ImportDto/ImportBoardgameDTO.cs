using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ImportDto
{
    [XmlType("Boardgame")]
    public class ImportBoardgameDTO
    {
        [Required]
        [MaxLength(20)]
        [MinLength(10)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(1.00, 10.00)]
        public double Rating { get; set; }

        [Required]
        [Range(2018, 2023)]
        public int YearPublished { get; set; }

        [Required]
        [Range(0, 4)]
        public int CategoryType { get; set; }
        [Required]
        public string Mechanics { get; set; } = null!;
    }
}
