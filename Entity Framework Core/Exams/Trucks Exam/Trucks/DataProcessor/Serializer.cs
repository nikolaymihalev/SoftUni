namespace Trucks.DataProcessor
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Text;
    using System.Xml.Serialization;
    using Trucks.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportDespatcherDto[]), new XmlRootAttribute("Despatchers"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);
            
            using StringWriter sw = new StringWriter(sb);

            var despatchers = context.Despatchers.AsNoTracking()
                .Where(d=>d.Trucks.Any())
                .Select(d=>new ExportDespatcherDto 
                {
                    DespatcherName = d.Name,
                    TrucksCount = d.Trucks.Count(),
                    Trucks = d.Trucks
                            .OrderBy(t=>t.RegistrationNumber)
                            .ToArray()
                            .Select(t=>new ExportTruckDto 
                            {
                                RegistrationNumber = t.RegistrationNumber,
                                Make = t.MakeType.ToString()
                            })
                            .ToArray()
                })
                .OrderByDescending(d=>d.TrucksCount)
                .ThenBy(d=>d.DespatcherName)
                .ToArray();

            xmlSerializer.Serialize(sw, despatchers, ns);

            return sb.ToString().TrimEnd();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            var clients = context.Clients.AsNoTracking()
                .Where(c=>c.ClientsTrucks.Any(ct=>ct.Truck.TankCapacity>=capacity))
                .Select(c=>new 
                {
                    Name = c.Name,
                    Trucks = c.ClientsTrucks
                            .Where(ct => ct.Truck.TankCapacity >= capacity)
                            .OrderBy(t=>t.Truck.MakeType)
                            .ThenByDescending(ct=>ct.Truck.CargoCapacity)
                            .ToArray()
                            .Select(ct=>new 
                            {
                                TruckRegistrationNumber = ct.Truck.RegistrationNumber,
                                VinNumber = ct.Truck.VinNumber,
                                TankCapacity = ct.Truck.TankCapacity,
                                CargoCapacity = ct.Truck.CargoCapacity,
                                CategoryType = ct.Truck.CategoryType.ToString(),
                                MakeType = ct.Truck.MakeType.ToString(),
                            })
                            .ToArray()
                })
                .OrderByDescending(c=>c.Trucks.Length)
                .ThenBy(c=>c.Name)
                .Take(10)
                .ToArray();

            return JsonConvert.SerializeObject(clients,Formatting.Indented);
        }
    }
}
