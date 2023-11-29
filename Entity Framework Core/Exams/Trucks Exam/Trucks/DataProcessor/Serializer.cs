namespace Trucks.DataProcessor
{
    using AutoMapper.QueryableExtensions;
    using AutoMapper;
    using Data;
    using System.Text;
    using System.Xml.Serialization;
    using Trucks.DataProcessor.ExportDto;

    public class Serializer
    {
        public static string ExportDespatchersWithTheirTrucks(TrucksContext context)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TrucksProfile>();
            });

            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportDespatcherDto[]), new XmlRootAttribute("Despatchers"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter sw = new StringWriter(sb);

            ExportDespatcherDto[] despatcherDtos =
                context
                .Despatchers
                .Where(d => d.Trucks.Any())
                .ProjectTo<ExportDespatcherDto>(config)
                .OrderByDescending(d => d.TrucksCount)
                .ThenBy(d => d.Name)
                .ToArray();
            xmlSerializer.Serialize(sw, despatcherDtos, namespaces);
            return sb.ToString().TrimEnd();
        }

        public static string ExportClientsWithMostTrucks(TrucksContext context, int capacity)
        {
            throw new NotImplementedException();
        }
    }
}
