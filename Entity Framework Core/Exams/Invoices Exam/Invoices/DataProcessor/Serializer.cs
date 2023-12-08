namespace Invoices.DataProcessor
{
    using Invoices.Data;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<InvoicesProfile>();
            });

            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientExportDto[]), new XmlRootAttribute("Clients"));

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using StringWriter sw = new StringWriter(sb);

            ClientExportDto[] clientsDtos = context
                .Clients
                .Where(c => c.Invoices.Any(ci => ci.IssueDate >= date))
                .ProjectTo<ClientExportDto>(config)
                .OrderByDescending(c => c.InvoicesCount)
                .ThenBy(c => c.Name)
                .ToArray();
            xmlSerializer.Serialize(sw, clientsDtos, namespaces);
            return sb.ToString().TrimEnd();
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {

            throw new NotImplementedException();
        }
    }
}