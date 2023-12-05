namespace Footballers.DataProcessor
{
    using Data;
    using Footballers.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            throw new NotImplementedException();
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
                .ToArray();

            return JsonConvert.SerializeObject(teams, Formatting.Indented);
        }
    }
}
