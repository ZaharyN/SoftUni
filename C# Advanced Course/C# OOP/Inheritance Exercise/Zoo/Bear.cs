using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoo
{
    public class Bear : Mammal
    {
        public Bear(string name) : base(name)
        {
            Name = name;
        }
    }
}
