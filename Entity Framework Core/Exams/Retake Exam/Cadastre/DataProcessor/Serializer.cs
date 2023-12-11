using Cadastre.Data;

namespace Cadastre.DataProcessor
{
    public class Serializer
    {
        public static string ExportPropertiesWithOwners(CadastreContext dbContext)
        {
           DateTime date;
            bool isDateValid = DateTime.TryParseExact(
                "01/01/2000",
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);

            var properties = dbContext.Properties.AsNoTracking()
                .Where(x=>x.DateOfAcquisition>=date)
                .OrderByDescending(x => x.DateOfAcquisition)
                .ThenBy(x => x.PropertyIdentifier)
                .Select(x=> new 
                {
                    PropertyIdentifier = x.PropertyIdentifier,
                    Area = x.Area,
                    Address = x.Address,
                    DateOfAcquisition = x.DateOfAcquisition.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Owners = x.PropertiesCitizens
                                .OrderBy(x=>x.Citizen.LastName)
                                .ToArray()
                                .Select(x=> new 
                                {
                                    LastName = x.Citizen.LastName,
                                    MaritalStatus = x.Citizen.MaritalStatus.ToString(),
                                }).ToArray()
                })                
                .ToArray();

            return JsonConvert.SerializeObject(properties,Formatting.Indented);
        }

        public static string ExportFilteredPropertiesWithDistrict(CadastreContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
