using _4.WildFarm.Core.Interfaces;
using _4.WildFarm.IO;
using _4.WildFarm.IO.Interfaces;
using _4.WildFarm.Models;
using _4.WildFarm.Models.Animals;
using _4.WildFarm.Models.Food;
using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        
        public Engine(IReader reader, IWriter writer) 
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            List<IAnimal> animals = new List<IAnimal>();

            string input = string.Empty;

            while((input = reader.ReadLine()) != "End")
            {
                IAnimal animal = InstantiateNewAnimal(input);
                writer.WriteLine(animal.ProduceSound());

                animals.Add(animal);

                // Second input:
                input = reader.ReadLine();
                
                IFood currentFood = InstantiateNewFood(input);

                try
                {
                    animal.Eat(currentFood);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
            foreach (var animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }

        static IAnimal InstantiateNewAnimal (string input)
        {
            string[] animalInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string type = animalInfo[0];

            if(type == "Hen")
            {
                return new Hen(animalInfo[1], double.Parse(animalInfo[2]), double.Parse(animalInfo[3]));
            }
            else if(type == "Owl")
            {
                return new Owl(animalInfo[1], double.Parse(animalInfo[2]), double.Parse(animalInfo[3]));
            }
            else if (type == "Mouse")
            {
                return new Mouse(animalInfo[1], double.Parse(animalInfo[2]), animalInfo[3]);
            }
            else if (type == "Cat")
            {
                return new Cat(animalInfo[1], double.Parse(animalInfo[2]), animalInfo[3], animalInfo[4]);
            }
            else if (type == "Dog")
            {
                return new Dog(animalInfo[1], double.Parse(animalInfo[2]), animalInfo[3]);
            }
            else if (type == "Tiger")
            {
                return new Tiger(animalInfo[1], double.Parse(animalInfo[2]), animalInfo[3], animalInfo[4]);
            }
            else
            {
                throw new ArgumentException("No animal can be instantiated!");
            }
        }

        static IFood InstantiateNewFood(string input)
        {
            string[] foodInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string foodType = foodInfo[0];
            int quantity = int.Parse(foodInfo[1]);

            if(foodType == "Fruit")
            {
                return new Fruit(quantity);
            }
            else if(foodType == "Meat")
            {
                return new Meat(quantity);
            }
            else if (foodType == "Seeds")
            {
                return new Seeds(quantity);
            }
            else if (foodType == "Vegetable")
            {
                return new Vegetable(quantity);
            }
            else
            {
                throw new ArgumentException("No food to instantiate");
            }
        }
    }
}
