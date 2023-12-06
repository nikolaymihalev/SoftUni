namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            throw new NotImplementedException();
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
                                .OrderBy(c=>c.Client.Name)
                                .ToArray()
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