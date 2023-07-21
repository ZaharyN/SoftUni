using _4.WildFarm.Models.Food;
using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Models.Animals
{
    public class Mouse : Mammal
    {
        private const double MouseWeightConstant = 0.10;

        public Mouse(string name, double weight, string livingregion)
            : base(name, weight, livingregion)
        {
        }

        protected override IReadOnlyCollection<Type> PrefferredFoods =>
            new HashSet<Type>()
            {
                typeof(Vegetable),
                typeof(Fruit),
            };

        protected override double WeightConstant => MouseWeightConstant;

        public override string ProduceSound() => "Squeak";

        public override string ToString() => $"{this.GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
    }
}
