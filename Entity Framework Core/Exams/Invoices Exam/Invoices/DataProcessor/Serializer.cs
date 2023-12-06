namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Invoices.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportClientDto[]), new XmlRootAttribute("Clients"));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            using StringWriter stringWriter = new StringWriter(sb);

            var clients = context.Clients.AsNoTracking()
                .Where(c=>c.Invoices.Any(i=>i.IssueDate>=date))
                .Select(c=>new ExportClientDto
                {
                    ClientName = c.Name,
                    VatNumber = c.NumberVat,
                    InvoicesCount = c.Invoices.Count(),
                    Invoices = c.Invoices
                            .Where(i => i.IssueDate >= date)
                            .OrderBy(i=>i.IssueDate)
                            .ThenByDescending(i=>i.DueDate)
                            .ToArray()
                            .Select(i=>new ExportInvoicesDto 
                            {
                                InvoiceNumber = i.Number,
                                InvoiceAmount = i.Amount,
                                DueDate = i.DueDate.ToString("d", CultureInfo.InvariantCulture),
                                Currency = i.CurrencyType.ToString()
                            })
                            .ToArray()
                })
                .OrderByDescending(c=>c.InvoicesCount)
                .ThenBy(c=>c.ClientName)
                .ToArray();

            xmlSerializer.Serialize(stringWriter, clients, ns);

            return sb.ToString().TrimEnd();
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            var products = context.Products.AsNoTracking()
                .Where(p=>p.ProductsClients.Any(pc=>pc.Client.Name.Length>=nameLength))
                .Select(p=>new 
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                                .Where(pc => pc.Client.Name.Length >= nameLength)
                                .ToArray()
                                .OrderBy(c=>c.Client.Name)
                                .Select(c=>new 
                                {
                                    Name = c.Client.Name,
                                    NumberVat = c.Client.NumberVat
                                })
                                .ToArray()
                })
                .OrderByDescending(p=>p.Clients.Length)
                .ThenBy(p=>p.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }
    }
}