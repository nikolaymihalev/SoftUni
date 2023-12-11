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
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPropertyDto[]), new XmlRootAttribute("Properties"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter sw = new StringWriter(sb);

            var properties = dbContext.Properties.AsNoTracking()
                .Where(x=>x.Area>=100)
                .OrderByDescending(x => x.Area)
                .ThenBy(x => x.DateOfAcquisition)
                .Select(x=> new ExportPropertyDto 
                {
                    PropertyIdentifier = x.PropertyIdentifier,
                    Area = x.Area.ToString(),
                    DateOfAcquisition = x.DateOfAcquisition.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    PostalCode = x.District.PostalCode
                })                
                .ToArray();
            xmlSerializer.Serialize(sw, properties, ns);

            return sb.ToString().TrimEnd();
        }
    }
}
