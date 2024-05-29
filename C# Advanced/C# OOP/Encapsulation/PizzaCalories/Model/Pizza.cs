using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories.Model
{
    public class Pizza
    {
        private string name;
        private List<Topping> toppings;

        public Pizza(string name, Dough dough)
        {
            Name = name;
            Dough = dough;
            Toppings = new List<Topping>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (value.Length < 1 || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                name = value;
            }
        }
        public List<Topping> Toppings
        {
            get => toppings;
            private set
            {
                toppings = value;
            }
        }
        public Dough Dough
        {
            get; private set;
        }

        public void AddTopping(Topping topping)
        {
            if (Toppings.Count == 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }
            Toppings.Add(topping);
        }
        public double TotalCalories => Dough.Calories + Toppings.Sum(x => x.Calories);
        public override string ToString() => $"{Name} - {TotalCalories:f2} Calories.";

        //Meatless - 370.00 Calories.
    }
}
