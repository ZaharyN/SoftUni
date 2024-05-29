using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories.Model
{
    public class Topping
    {
        private const double BaseCalories = 2.0;
        private readonly Dictionary<string, double> names;

        private double toppingWeight;
        private string name;

        public Topping(string name, double toppingWeight)
        {
            names = new Dictionary<string, double>
            {
                { "meat", 1.2 },
                { "veggies", 0.8 },
                { "cheese", 1.1 },
                { "sauce", 0.9 }
            };

            Name = name;
            ToppingWeight = toppingWeight;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (!names.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                name = value;
            }
        }

        public double ToppingWeight
        {
            get => toppingWeight;
            private set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{Name} weight should be in the range [1..50].");
                }
                toppingWeight = value;
            }
        }

        public double Calories => ToppingWeight * BaseCalories * names[Name.ToLower()];
    }
}
