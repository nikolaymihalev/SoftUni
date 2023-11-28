namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.DataProcessor.ExportDto;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportCreatorDTO[]), new XmlRootAttribute("Creators"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            using StringWriter sw = new StringWriter(sb);

            ExportCreatorDTO[] creators = context.Creators
                .Where(c => c.Boardgames.Any())
                .Select(c => new ExportCreatorDTO
                {
                    CreatorName = c.FirstName + " " + c.LastName,
                    BoardgamesCount = c.Boardgames.Count(),
                    Boardgames = c.Boardgames.Select(b => new ExportCreatorBoardgamesDTO
                    {
                        BoardgameName = b.Name,
                        BoardgameYearPublished = b.YearPublished
                    })
                    .OrderBy(b => b.BoardgameName)
                    .ToArray()
                })
                .OrderByDescending(c => c.BoardgamesCount)
                .ThenBy(c => c.CreatorName)
                .ToArray();

            xmlSerializer.Serialize(sw, creators, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            throw new NotImplementedException();
        }
    }
}