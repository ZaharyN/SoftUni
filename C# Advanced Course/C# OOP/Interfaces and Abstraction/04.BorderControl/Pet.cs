using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.BorderControl
{
    public class Pet : INameable, IBirthable
    {
        public Pet(string name, string birthday)
        {
            Name = name;
            Birthday = birthday;
        }
        public string Name
        {
            get;
            private set;
        }
        public string Birthday
        {
            get;
            private set;
        }
    }
}
