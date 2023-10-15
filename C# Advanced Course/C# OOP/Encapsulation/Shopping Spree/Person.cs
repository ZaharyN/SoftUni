using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3.ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> products;

        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            Products = new List<Product>();
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }
        public decimal Money
        {
            get => money;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }
        public List<Product> Products
        {
            get { return products; }
            set { products = value; }
        }

        public string AddProduct(Product product)
        {
            if (Money >= product.Cost)
            {
                Money -= product.Cost;
                Products.Add(product);
                return $"{this.Name} bought {product.Name}";
            }
            return $"{this.Name} can't afford {product.Name}";
        }
    }
}
