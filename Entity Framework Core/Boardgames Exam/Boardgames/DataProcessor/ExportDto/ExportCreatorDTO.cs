using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ExportDto
{
    [XmlType("Creator")]
    public class ExportCreatorDTO
    {
        public string CreatorName { get; set; } = null!;

        [XmlAttribute]
        public int BoardgamesCount { get; set; }

        [XmlArray("Boardgames")]
        public ExportCreatorBoardgamesDTO[] Boardgames { get; set; } = null!;

    }
}
