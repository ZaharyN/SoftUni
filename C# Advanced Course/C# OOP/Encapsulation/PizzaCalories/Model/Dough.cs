using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories.Model
{
    public class Dough
    {
        private const double BaseCalorie = 2.0;
        private readonly Dictionary<string, double> flourTypesCalories;
        private readonly Dictionary<string, double> bakingTechniqueCalories;
        //•	White - 1.5
        //•	Wholegrain - 1.0

        //•	Crispy - 0.9
        //•	Chewy - 1.1
        //•	Homemade - 1.0

        private string flourType;
        private string bakingTechnique;
        private double weight;

        public Dough(string flourType,string bakingTechnique, double weight)
        {

            flourTypesCalories =
            new Dictionary<string, double>
            {
                { "white", 1.5 },
                { "wholegrain", 1.0 }
            };

            bakingTechniqueCalories
                = new Dictionary<string, double>
            {
                {"crispy", 0.9 },
                {"chewy", 1.1 },
                {"homemade", 1.0 },
            };

            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }

        public string FlourType
        {
            get => flourType;
            private set
            {
                if (!flourTypesCalories.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                flourType = value.ToLower();
            }
        }
        public string BakingTechnique
        {
            get => bakingTechnique;
            private set
            {
                if (!bakingTechniqueCalories.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingTechnique = value.ToLower();
            }
        }
        public double Weight
        {
            get => weight;
            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        }
        public double Calories
        {
            get => Weight
                * BaseCalorie
                * flourTypesCalories[FlourType.ToLower()]
                * bakingTechniqueCalories[BakingTechnique.ToLower()];
        }
    }
}
