namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Invoices.Data.Models.Enums;
    using Invoices.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            StringBuilder sb = new();

            ExportClientDTO[] clients = context.Clients
                .Where(c => c.Invoices.Any(i => i.IssueDate >= date))
                .ToArray()
                .Select(c => new ExportClientDTO
                {
                    InvoicesCount = c.Invoices.Count,
                    Name = c.Name,
                    NumberVat = c.NumberVat,
                    Invoices = c.Invoices.Select(i => new ExportInvoiceDTO
                    {
                        Number = i.Number,
                        Amount = i.Amount,
                        DueDate = i.DueDate.ToString("d", CultureInfo.InvariantCulture),
                        CurrencyType = i.CurrencyType
                    })
                    .OrderByDescending(i => i.DueDate)
                    .ToArray()
                })
                .OrderByDescending(c => c.Invoices.Count())
                    .ThenBy(c => c.Name)
                .ToArray();

            XmlSerializer serializer = 
                new XmlSerializer(typeof(ExportClientDTO[]), new XmlRootAttribute("Clients"));
            XmlSerializerNamespaces xmlns = new();
            xmlns.Add(string.Empty, string.Empty);

            using (StringWriter sw = new(sb))
            {
                serializer.Serialize(sw, clients, xmlns);
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            var products = context.Products
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                        .Where(pc => pc.Client.Name.Length >= nameLength)
                        .Select(pc => new
                        {
                            Name = pc.Client.Name,
                            NumberVat = pc.Client.NumberVat
                        })
                        .OrderBy(c => c.Name)
                        .ToArray()
                })
                .OrderByDescending(p => p.Clients.Count())
                    .ThenBy(p => p.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }
    }
}