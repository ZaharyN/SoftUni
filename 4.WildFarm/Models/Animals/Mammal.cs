using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Models.Animals
{
    public abstract class Mammal : Animal, IMammal
    {
        public Mammal(string name, double weight, string livingregion)
            : base (name, weight)
        {
            LivingRegion = livingregion;
        }
        public string LivingRegion { get; private set; }
    }
}
