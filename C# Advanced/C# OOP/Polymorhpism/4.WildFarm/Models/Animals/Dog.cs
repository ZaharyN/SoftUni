using _4.WildFarm.Models.Food;
using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Models.Animals
{
    public class Dog : Mammal
    {
        private const double DogWeightConstant = 0.4;
        
        public Dog(string name, double weight, string livingregion)
            : base(name, weight, livingregion)
        {
        }

        protected override IReadOnlyCollection<Type> PrefferredFoods =>
            new HashSet<Type>()
            {
                typeof(Meat),
            };
        protected override double WeightConstant => DogWeightConstant;

        public override string ProduceSound() => "Woof!";

        public override string ToString() => $"{this.GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
    }
}
