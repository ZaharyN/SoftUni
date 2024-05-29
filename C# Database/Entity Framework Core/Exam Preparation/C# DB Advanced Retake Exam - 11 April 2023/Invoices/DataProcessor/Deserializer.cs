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
            StringReader reader = new StringReader(xmlString);

            XmlSerializer serializer =
                new XmlSerializer(typeof(ImportClientDTO[]), new XmlRootAttribute("Clients"));

            ImportClientDTO[] clientsDTO = serializer.Deserialize(reader) as ImportClientDTO[];

            List<Client> clients = new List<Client>();

            foreach (var clientDTO in clientsDTO)
            {
                if (!IsValid(clientDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client client = new Client()
                {
                    Name = clientDTO.Name,
                    NumberVat = clientDTO.NumberVat,
                };

                foreach (var addressDTO in clientDTO.Addresses)
                {
                    if (!IsValid(addressDTO))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    Address address = new Address
                    {
                        StreetName = addressDTO.StreetName,
                        StreetNumber = addressDTO.StreetNumber,
                        PostCode = addressDTO.PostCode,
                        City = addressDTO.City,
                        Country = addressDTO.Country
                    };

                    client.Addresses.Add(address);
                }
                sb.AppendLine(string.Format(SuccessfullyImportedClients, client.Name));
                clients.Add(client);
            }
            context.Clients.AddRange(clients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new();

            ImportInvoiceDTO[] invoicesDTO = JsonConvert.DeserializeObject<ImportInvoiceDTO[]>(jsonString);

            List<Invoice> invoices = new List<Invoice>();

            foreach (var invoiceDTO in invoicesDTO)
            {
                if (!IsValid(invoiceDTO)
                    || !DateTime.TryParse(invoiceDTO.IssueDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime issueDate)
                    || !DateTime.TryParse(invoiceDTO.DueDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate)
                    || dueDate < issueDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Invoice invoice = new Invoice()
                {
                    Number = invoiceDTO.Number,
                    IssueDate = issueDate,
                    DueDate = dueDate,
                    Amount = invoiceDTO.Amount,
                    CurrencyType = (CurrencyType)invoiceDTO.CurrencyType,
                    ClientId = invoiceDTO.ClientId
                };

                invoices.Add(invoice);
                sb.AppendLine(string.Format(SuccessfullyImportedInvoices, invoice.Number));
            }
            context.Invoices.AddRange(invoices);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new();

            ImportProductDTO[] productsDTO = JsonConvert.DeserializeObject<ImportProductDTO[]>(jsonString);

            List<Product> products = new List<Product>();

            int[] clientIds = context.Clients.Select(c => c.Id).ToArray();

            foreach (var productDTO in productsDTO)
            {
                if (!IsValid(productDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Product product = new Product()
                {
                    Name = productDTO.Name,
                    Price = productDTO.Price,
                    CategoryType = (CategoryType)productDTO.CategoryType
                };

                foreach (var clientId in productDTO.Clients.Distinct())
                {
                    if (!clientIds.Contains(clientId))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    product.ProductsClients.Add(new ProductClient()
                    {
                        ClientId = clientId
                    });

                    //context.ProductsClients.Add(new ProductClient()
                    //{
                    //    ClientId = clientId,
                    //    ProductId = product.Id
                    //});
                }
                products.Add(product);
                sb.AppendLine(
                    string.Format(SuccessfullyImportedProducts, product.Name, product.ProductsClients.Count()));
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
