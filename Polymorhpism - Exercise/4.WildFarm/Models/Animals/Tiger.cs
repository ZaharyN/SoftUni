using _4.WildFarm.IO;
using _4.WildFarm.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Models.Animals
{
    public class Tiger : Feline
    {
        private const double tigerWeightConstant = 1.0;
        public Tiger(string name, double weight, string livingregion, string breed)
            : base(name, weight, livingregion, breed)
        {
        }

        protected override IReadOnlyCollection<Type> PrefferredFoods =>
            new HashSet<Type>()
            {
                typeof(Meat)
            };

        protected override double WeightConstant => tigerWeightConstant;

        public override string ProduceSound() => "ROAR!!!";
    }
}
