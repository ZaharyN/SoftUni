using _4.WildFarm.Models.Food;
using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Models.Animals
{
    public class Owl : Bird
    {
        private const double OwlWeightConstant = 0.25;
        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        protected override IReadOnlyCollection<Type> PrefferredFoods =>
            new HashSet<Type>()
            {
                typeof(Meat)
            };

        protected override double WeightConstant => OwlWeightConstant;

        public override string ProduceSound() => "Hoot Hoot";
        public override string ToString() => $"{this.GetType().Name} [{this.Name}, {WingSize}, {Weight}, {FoodEaten}]";

    }
}
