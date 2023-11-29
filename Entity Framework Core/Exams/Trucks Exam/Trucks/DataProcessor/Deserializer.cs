namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportDespatchersDTO[]),new XmlRootAttribute("Despatchers"));

            using StringReader stream = new StringReader(xmlString);
            ImportDespatchersDTO[] importDespatchersDTOs = xmlSerializer.Deserialize(stream) as ImportDespatchersDTO[];

            List<Despatcher> despatchers = new List<Despatcher>();

            foreach (var dto in importDespatchersDTOs) 
            {
                if (!IsValid(dto)||string.IsNullOrEmpty(dto.Position)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Despatcher despatcherToAdd = new Despatcher() 
                {
                    Name = dto.Name,
                    Position = dto.Position
                };

                foreach (var truckDto in dto.Trucks) 
                {
                    if (!IsValid(truckDto)) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Truck truckToAdd = new Truck() 
                    {
                        RegistrationNumber = truckDto.RegistrationNumber,
                        VinNumber = truckDto.VinNumber,
                        TankCapacity = truckDto.TankCapacity,
                        CargoCapacity = truckDto.CargoCapacity,
                        CategoryType = (CategoryType)truckDto.CategoryType,
                        MakeType = (MakeType)truckDto.MakeType
                    };

                    despatcherToAdd.Trucks.Add(truckToAdd);
                }

                despatchers.Add(despatcherToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, despatcherToAdd.Name, despatcherToAdd.Trucks.Count));
            }

            context.Despatchers.AddRange(despatchers);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportClientDTO[] importClientDTOs = JsonConvert.DeserializeObject<ImportClientDTO[]>(jsonString);

            List<Client> clients = new List<Client>();

            foreach (var dto in importClientDTOs) 
            {
                if (!IsValid(dto)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client clientToAdd = new Client() 
                {
                    Name = dto.Name,
                    Nationality = dto.Nationality,
                    Type = dto.Type
                };


                foreach(int id in dto.Trucks.Distinct()) 
                {
                    Truck truck = context.Trucks.First(x => x.Id == id);
                    if (truck == null) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    clientToAdd.ClientsTrucks.Add(new ClientTruck() { Truck = truck });
                }

                clients.Add(clientToAdd);
                sb.AppendLine(string.Format(SuccessfullyImportedClient, clientToAdd.Name, clientToAdd.ClientsTrucks.Count));
            }


            context.Clients.AddRange(clients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}