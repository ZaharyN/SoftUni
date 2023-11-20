using AutoMapper;
using Castle.Core.Configuration.Xml;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ProductShop.Data;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;
using System.Runtime.Serialization;
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
            Console.WriteLine(GetProductsInRange(context));
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
            Product[] products = mapper.Map<Product[]>(importProductsDTO);
            
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
            Mapper mapper = GetMapper();

           //var carsToExport = context.Products
           //    .Select()

            return string.Empty;
        }
    }
}