using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Configuration.Xml;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();

            //01.
            //string xmlInput = File.ReadAllText("../../../Datasets/users.xml");
            //Console.WriteLine(ImportUsers(context,xmlInput));
            //02.
            //string productsInput = File.ReadAllText("../../../Datasets/products.xml");
            //Console.WriteLine(ImportProducts(context, productsInput));
            //03.
            //string inputXml = File.ReadAllText("../../../Datasets/categories.xml");
            //Console.WriteLine(ImportCategories(context,inputXml));
            //04.
            //string inputXml = File.ReadAllText("../../../Datasets/categories-products.xml");
            //Console.WriteLine(ImportCategoryProducts(context, inputXml));
            //05.
            Console.WriteLine(GetUsersWithProducts(context));
        }
        private static Mapper GetMapper()
        {
            MapperConfiguration configuration
                = new MapperConfiguration(cfg => cfg.AddProfile<ProductShopProfile>());

            return new Mapper(configuration);
        }

        //01.
        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            //1. Create xml serializer:
            XmlSerializer serializer = new XmlSerializer(typeof(ImportUserDTO[]), new XmlRootAttribute("Users"));

            //2. Create stream reader:
            using StringReader reader = new StringReader(inputXml);

            ImportUserDTO[] importUsersDTO = (ImportUserDTO[])serializer.Deserialize(reader);

            //3. Mapping to real object:
            Mapper mapper = GetMapper();
            User[] users = mapper.Map<User[]>(importUsersDTO);


            context.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Length}";
        }

        //02.
        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportProductDTO[]),
                new XmlRootAttribute("Products"));

            using StringReader reader = new StringReader(inputXml);
            ImportProductDTO[] importProductsDTO = (ImportProductDTO[])serializer.Deserialize(reader);

            Mapper mapper = GetMapper();
            Models.Product[] products = mapper.Map<Models.Product[]>(importProductsDTO);

            context.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Length}";
        }

        //03.
        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCategoryDTO[]),
                new XmlRootAttribute("Categories"));

            StringReader reader = new StringReader(inputXml);
            ImportCategoryDTO[] importCategoriesDTO = serializer.Deserialize(reader) as ImportCategoryDTO[];

            Mapper mapper = GetMapper();
            Category[] categories = mapper.Map<Category[]>(importCategoriesDTO);

            Category[] validCategories = categories
                    .Where(x => x.Name != null)
                    .ToArray();

            context.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {categories.Length}";
        }

        //04.
        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ImportCategoryProductDTO[]),
                new XmlRootAttribute("CategoryProducts"));

            StringReader reader = new StringReader(inputXml);
            ImportCategoryProductDTO[] importCategoryProductsDTO =
                serializer.Deserialize(reader) as ImportCategoryProductDTO[];

            Mapper mapper = GetMapper();
            CategoryProduct[] categoryProducts = mapper.Map<CategoryProduct[]>(importCategoryProductsDTO);

            context.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Length}";
        }

        //05.
        public static string GetProductsInRange(ProductShopContext context)
        {
            ExportProductsByPriceRange[] productsWithPriceRange = context.Products
                 .Where(p => p.Price > 500 && p.Price <= 1000)
                 .Select(p => new ExportProductsByPriceRange
                 {
                     Name = p.Name,
                     Price = p.Price,
                     BuyerFullName = p.Buyer.FirstName + " " + p.Buyer.LastName
                 })
                 .OrderBy(p => p.Price)
                 .Take(10)
                 .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportProductsByPriceRange[]),
                new XmlRootAttribute("Products"));

            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, productsWithPriceRange, xmlns);
            }

            return sb.ToString();
        }

        //06.
        public static string GetSoldProducts(ProductShopContext context)
        {
            ExportUsersWithSales[] usersWithSales = context.Users
                .Where(u => u.ProductsSold.Any())
                .Select(u => new ExportUsersWithSales
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    ProductsSold = u.ProductsSold.Select(ps => new ExportProduct
                    {
                        Name = ps.Name,
                        Price = ps.Price
                    }).ToArray(),
                })
                .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                .Take(5)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportUsersWithSales[]),
                new XmlRootAttribute("Users"));

            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            StringBuilder sb = new();
            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, usersWithSales, xmlns);
            }

            return sb.ToString().TrimEnd();
        }

        //07.
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            ExportCategories[] categories = context.Categories
                .Select(c => new ExportCategories
                {
                    Name = c.Name,
                    Count = c.CategoryProducts.Count,
                    AveragePrice = c.CategoryProducts.Average(cp => cp.Product.Price),
                    TotalRevenue = c.CategoryProducts.Sum(cp => cp.Product.Price)
                })
                .OrderByDescending(c => c.Count)
                    .ThenBy(c => c.TotalRevenue)
                .ToArray();

            XmlSerializer serializer = new XmlSerializer(typeof(ExportCategories[]),
                new XmlRootAttribute("Categories"));

            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, categories, xmlns);
            }

            return sb.ToString().TrimEnd();
        }

        //08.
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithProducts = context.Users
                .Where(u => u.ProductsSold.Any())
                .Select(u => new UserInfoDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductDTO()
                    {
                        Count = u.ProductsSold.Count(),
                        Products = u.ProductsSold.Select(ps => new ProductDTO()
                        {
                            Name = ps.Name,
                            Price = ps.Price
                        })
                        .OrderByDescending(ps => ps.Price)
                        .ToArray()
                    }
                })
                .OrderByDescending(u => u.SoldProducts.Count)
                .Take(10)
                .ToArray();

            ExportUserDTO exportUsers = new ExportUserDTO()
            {
                UsersCount = context.Users.Count(u => u.ProductsSold.Any()),
                Users = usersWithProducts
            };

            XmlSerializer serializer = new XmlSerializer(typeof(ExportUserDTO),
                new XmlRootAttribute("Users"));

            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            StringBuilder sb = new();

            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, exportUsers, xmlns);
            }

            return sb.ToString().TrimEnd();
        }

        private static string XmlSerializer<T>(T dto, string xmlRootAttribute)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T),
                new XmlRootAttribute(xmlRootAttribute));

            XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            StringBuilder sb = new StringBuilder();
            using(StringWriter sw = new(sb))
            {
                try
                {
                    serializer.Serialize(sw, dto, xmlns);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Could not serialize dto.");
                }
            }
            return sb.ToString().TrimEnd();
        }
    }
}