using _4.WildFarm.Models.Food;
using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        private const double CatWeightConstant = 0.3;
        
        public Cat(string name, double weight, string livingregion, string breed)
            : base(name, weight, livingregion, breed)
        {
        }

        protected override double WeightConstant => CatWeightConstant;

        protected override IReadOnlyCollection<Type> PrefferredFoods =>
            new HashSet<Type>()
            {
                typeof(Vegetable),
                typeof(Meat)
            };

        public override string ProduceSound() => "Meow";
    }
}
