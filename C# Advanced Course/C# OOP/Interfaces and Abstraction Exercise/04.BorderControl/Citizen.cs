using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace _04.BorderControl
{
    public class Citizen : IIdentifiable, IBirthable, IBuyer
    {
        public Citizen(string name, int age, string id, string birthday)
        {
            Name = name;
            Age = age;
            ID = id;
            Birthday = birthday;
        }

        public string Name
        {
            get; private set;
        }

        public int Age
        {
            get; private set;
        }

        public string ID
        {
            get; private set;
        }

        public string Birthday
        {
            get;
            private set;
        }

        public int Food
        {
            get;
            private set;
        }
        public void BuyFood()
        {
            Food += 10;
        }
    }
}
