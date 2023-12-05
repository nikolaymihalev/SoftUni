namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCoachDTO[]), new XmlRootAttribute("Coaches"));
            using StringReader reader = new StringReader(xmlString);

            ImportCoachDTO[] importCoachDTOs = xmlSerializer.Deserialize(reader) as ImportCoachDTO[];
            List<Coach> coaches = new List<Coach>();

            foreach (var dto in importCoachDTOs) 
            {
                if (!IsValid(dto)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (string.IsNullOrEmpty(dto.Nationality)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Coach coach = new Coach() 
                {
                    Name = dto.Name,
                    Nationality = dto.Nationality
                };

                foreach (var footballer in dto.Footballers) 
                {
                    if (!IsValid(footballer))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime contractStart;
                    bool isStartDateValid = DateTime.TryParseExact(
                        footballer.ContractStartDate, 
                        "dd/MM/yyyy", 
                        CultureInfo.InvariantCulture, 
                        DateTimeStyles.None, 
                        out contractStart);

                    if (!isStartDateValid) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime contractEnd;
                    bool isEndDateValid = DateTime.TryParseExact(
                       footballer.ContractEndDate,
                       "dd/MM/yyyy",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out contractEnd);

                    if (!isEndDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (contractStart >= contractEnd) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer footballerToAdd = new Footballer 
                    {
                        Name = footballer.Name,
                        ContractStartDate = contractStart,
                        ContractEndDate = contractEnd,
                        BestSkillType = (BestSkillType)footballer.BestSkillType,
                        PositionType = (PositionType)footballer.PositionType
                    };

                    coach.Footballers.Add(footballerToAdd);
                }
                coaches.Add(coach);
                sb.AppendLine(string.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
            }
            context.Coaches.AddRange(coaches); 
            context.SaveChanges(); 

            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
