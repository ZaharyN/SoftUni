using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4.WildFarm.Models.Food;
using System.Collections.ObjectModel;

namespace _4.WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        private const double HenWeightConstant = 0.35;
        
        public Hen(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        protected override IReadOnlyCollection<Type> PrefferredFoods =>
            new HashSet<Type>()
            {
                typeof(Vegetable),
                typeof(Seeds),
                typeof(Meat),
                typeof(Fruit)
            };

        protected override double WeightConstant => HenWeightConstant;

        public override string ProduceSound() => "Cluck";
        public override string ToString() => $"{this.GetType().Name} [{Name}, {WingSize}, {Weight}, {FoodEaten}]";

    }
}
