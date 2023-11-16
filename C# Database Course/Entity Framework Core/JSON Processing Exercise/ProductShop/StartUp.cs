using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using System.Text.Json;
using System.Xml.XPath;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            //string usersJson = File.ReadAllText("../../../Datasets/users.json");
            //string productsJson = File.ReadAllText("../../../Datasets/products.json");
            //string categoriesJson = File.ReadAllText("../../../Datasets/categories.json");
            //string categoryProductsJson = File.ReadAllText("../../../Datasets/categories-products.json");

            Console.WriteLine(GetUsersWithProducts(context));
        }

        //01.
        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            User[] users = JsonConvert.DeserializeObject<User[]>(inputJson);

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }

        //02.
        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            Product[] products = JsonConvert.DeserializeObject<Product[]>(inputJson);

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";

        }

        //03.
        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            Category[] categories = JsonConvert.DeserializeObject<Category[]>(inputJson);
            Category[] validCategories = categories
                .Where(c => c.Name is not null)
                .ToArray();

            context.Categories.AddRange(validCategories);
            context.SaveChanges();

            return $"Successfully imported {validCategories.Count()}";
        }

        //04.
        public static string ImportCategoryProducts(ProductShopContext context, string inputJson)
        {
            CategoryProduct[] categoryProducts = JsonConvert.DeserializeObject<CategoryProduct[]>(inputJson);

            context.CategoriesProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {categoryProducts.Count()}";
        }

        //05.
        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .OrderBy(p => p.price)
                .ToArray();

            string result = JsonConvert.SerializeObject(products, Formatting.Indented);

            return result;
        }

        //06.
        public static string GetSoldProducts(ProductShopContext context)
        {
            var soldProducts = context.Users
                .Include(u => u.ProductsSold)
                .Where(u => u.ProductsSold.Any(ps => ps.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Select(ps => new
                        {
                            name = ps.Name,
                            price = ps.Price,
                            buyerFirstName = ps.Buyer.FirstName,
                            buyerLastName = ps.Buyer.LastName
                        })
                })
                .OrderBy(u => u.lastName)
                    .ThenBy(u => u.firstName);

            string result = JsonConvert.SerializeObject(soldProducts, Formatting.Indented);

            return result;
        }

        //07.
        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var productsByCategory = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoriesProducts.Count(),
                    averagePrice = c.CategoriesProducts
                        .Average(cp => cp.Product.Price)
                        .ToString("f2"),
                    totalRevenue = c.CategoriesProducts
                        .Sum(cp => cp.Product.Price)
                        .ToString("f2")
                })
                .OrderByDescending(c => c.productsCount);
                
            string result = JsonConvert.SerializeObject (productsByCategory, Formatting.Indented);

            return result;
        }

        //08.
        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var usersWithProducts = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    products = u.ProductsSold
                    .Where(u => u.BuyerId != null)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price
                    }).ToList(),
                })
                .OrderByDescending(u => u.products.Count())
                .ToList();

            var result = new
            {
                usersCount = usersWithProducts.Count,
                users = usersWithProducts.Select(u => new
                {
                    u.firstName,
                    u.lastName,
                    u.age,
                    soldProducts = new
                    {
                        count = u.products.Count,
                        products = u.products
                    }
                })
            };

            string resultJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
                

            return resultJson;
        }

    }
}