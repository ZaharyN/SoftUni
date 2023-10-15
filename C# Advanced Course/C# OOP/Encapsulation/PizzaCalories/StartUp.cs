using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _04.PizzaCalories.Model;

namespace _04.PizzaCalories
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            try
            {
                string[] pizzaInfo = Console.ReadLine()
                .Split();

                string[] doughInfo = Console.ReadLine()
                .Split();

                Dough dough = new(doughInfo[1], doughInfo[2], double.Parse(doughInfo[3]));
                Pizza pizza = new(pizzaInfo[1], dough);

                string input;

                while ((input = Console.ReadLine()) != "END")
                {
                    string[] toppingInfo = input
                        .Split();

                    try
                    {
                        Topping topping = new(toppingInfo[1], double.Parse(toppingInfo[2]));
                        pizza.AddTopping(topping);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }
                }

                Console.WriteLine(pizza.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}