using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Models.Animals
{
    public abstract class Animal : IAnimal
    {
        protected Animal(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public string Name
        {
            get;
            private set;
        }
        public double Weight
        {
            get;
            set;
        }
        protected abstract IReadOnlyCollection<Type> PrefferredFoods
        {
            get;
        }
        public int FoodEaten
        {
            get;
            set;
        }
        protected abstract double WeightConstant
        {
            get;
        }
        public void Eat(IFood food)
        {
            if (!PrefferredFoods.Any(x => x.Name == food.GetType().Name))
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }
            Weight += WeightConstant * food.Quantity;
            FoodEaten += food.Quantity;
        }

        public abstract string ProduceSound();

    }
}
