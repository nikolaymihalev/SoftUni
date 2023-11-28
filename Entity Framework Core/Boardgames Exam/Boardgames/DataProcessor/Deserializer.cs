namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCreatorDTO[]), new XmlRootAttribute("Creators"));

            using StringReader stream = new StringReader(xmlString);
            ImportCreatorDTO[] importCreatorDTOs = (ImportCreatorDTO[])xmlSerializer.Deserialize(stream);

            List<Creator> creators = new List<Creator>();

            foreach (var creator in importCreatorDTOs)
            {
                if (!IsValid(creator))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Creator creatorToAdd = new Creator()
                {
                    FirstName = creator.FirstName,
                    LastName = creator.LastName,
                };

                foreach (ImportBoardgameDTO boardgameDTO in creator.Boardgames)
                {
                    if (!IsValid(boardgameDTO))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame boardgame = new Boardgame()
                    {
                        Name = boardgameDTO.Name,
                        Rating = boardgameDTO.Rating,
                        YearPublished = boardgameDTO.YearPublished,
                        CategoryType = (CategoryType)boardgameDTO.CategoryType,
                        Mechanics = boardgameDTO.Mechanics
                    };

                    creatorToAdd.Boardgames.Add(boardgame);
                }
                creators.Add(creatorToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedCreator, creatorToAdd.FirstName, creatorToAdd.LastName, creatorToAdd.Boardgames.Count));
            }
            context.AddRange(creators);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
