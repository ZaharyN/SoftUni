using _4.WildFarm.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.Models.Animals
{
    public abstract class Bird : Animal, IBird
    {
        protected Bird(string name, double weight, double wingsize)
            : base(name, weight)
        {
            WingSize = wingsize;
        }

        public double WingSize
        {
            get;
            private set;
        }
    }
}
