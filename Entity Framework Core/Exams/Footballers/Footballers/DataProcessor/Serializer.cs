namespace Footballers.DataProcessor
{
    using Data;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportCoachDTO[]), new XmlRootAttribute("Coaches"));

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            using StringWriter sw = new StringWriter(sb);

            ExportCoachDTO[] coaches = context.Coaches.AsNoTracking()
                .Where(c=>c.Footballers.Any())
                .Select(c=> new ExportCoachDTO 
                {
                    CoachName = c.Name,
                    FootballersCount = c.Footballers.Count(),
                    Footballers = c.Footballers.Select(f=>new ExportFootballerDTO 
                                                {
                                                    Name = f.Name,
                                                    Position = f.PositionType.ToString()
                                                })
                                                .OrderBy(f=>f.Name)
                                                .ToArray()
                })
                .OrderByDescending(c=>c.FootballersCount)
                .ThenBy(c=>c.CoachName)
                .ToArray();

            xmlSerializer.Serialize(sw, coaches, ns);

            return sb.ToString().TrimEnd();
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teams = context.Teams.AsNoTracking()
                .Where(t=>t.TeamsFootballers.Any(tf=>tf.Footballer.ContractStartDate>=date))
                .Select(t=>new 
                {
                    Name = t.Name,
                    Footballers = t.TeamsFootballers
                                .Where(f => f.Footballer.ContractStartDate >= date)
                                .Select(f=>new 
                                {
                                    FootballerName = f.Footballer.Name,
                                    ContractStartDate = f.Footballer.ContractStartDate.ToString("d",CultureInfo.InvariantCulture),
                                    ContractEndDate = f.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                                    BestSkillType = f.Footballer.BestSkillType.ToString(),
                                    PositionType = f.Footballer.PositionType.ToString()
                                })
                                .OrderByDescending(f=>f.ContractEndDate)
                                .ThenBy(f => f.FootballerName)
                                .ToArray()                    
                })
                .OrderByDescending(t=>t.Footballers.Length)
                .ThenBy(x=>x.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }
    }
}
