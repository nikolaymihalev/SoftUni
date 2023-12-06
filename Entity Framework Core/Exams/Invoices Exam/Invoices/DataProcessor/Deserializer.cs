namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.Data.Models.Enums;
    using Invoices.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportClientDto[]), new XmlRootAttribute("Clients"));
            using StringReader reader = new StringReader(xmlString);

            ImportClientDto[] importClientDtos = xmlSerializer.Deserialize(reader) as ImportClientDto[];
            List<Client> clients = new List<Client>();

            foreach (var dto in importClientDtos)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client client = new Client()
                {
                    Name = dto.Name,
                    NumberVat = dto.NumberVat
                };

                foreach (var address in dto.Addresses)
                {
                    if (!IsValid(address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (string.IsNullOrEmpty(address.StreetName))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    client.Addresses.Add(new Address
                    {
                        StreetName = address.StreetName,
                        StreetNumber = address.StreetNumber,
                        PostCode = address.PostCode,
                        City = address.City,
                        Country = address.Country
                    });
                }
                clients.Add(client);
                sb.AppendLine(string.Format(SuccessfullyImportedClients, client.Name));
            }

            context.Clients.AddRange(clients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportInvoiceDto[] importInvoiceDtos = JsonConvert.DeserializeObject<ImportInvoiceDto[]>(jsonString);
            List<Invoice> invoices = new List<Invoice>();

            foreach (var dto in importInvoiceDtos)
            {
                if (!IsValid(dto)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime issueDate;
                bool isIssueDateValid = DateTime.TryParseExact(
                    dto.IssueDate, 
                    "yyyy-MM-dd", 
                    CultureInfo.InvariantCulture, 
                    DateTimeStyles.None,
                    out issueDate);

                if (!isIssueDateValid) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime dueDate;
                bool isDueDateValid = DateTime.TryParseExact(
                    dto.DueDate,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out dueDate);

                if (!isDueDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (issueDate>dueDate) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Invoice invoice = new Invoice
                {
                    Number = dto.Number,
                    IssueDate = issueDate,
                    DueDate = dueDate,
                    Amount = (decimal)dto.Amount,
                    CurrencyType = (CurrencyType)dto.CurrencyType,
                    ClientId = dto.ClientId
                };

                invoices.Add(invoice);
                sb.AppendLine(string.Format(SuccessfullyImportedInvoices,invoice.Number));
            }

            context.Invoices.AddRange(invoices);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportProductDto[] importProductDtos = JsonConvert.DeserializeObject<ImportProductDto[]>(jsonString);
            List<Product> products = new List<Product>();

            foreach(var dto in importProductDtos) 
            {
                if (!IsValid(dto)) 
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Product product = new Product 
                {
                    Name = dto.Name,
                    Price = (decimal)dto.Price,
                    CategoryType = (CategoryType)dto.CategoryType
                };

                foreach (int id in dto.Clients.Distinct()) 
                {
                    Client client = context.Clients.Find(id);
                    if (client == null) 
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    product.ProductsClients.Add(new ProductClient { Client = client});
                }

                products.Add(product);
                sb.AppendLine(string.Format(SuccessfullyImportedProducts,product.Name,product.ProductsClients.Count));
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    } 
}
