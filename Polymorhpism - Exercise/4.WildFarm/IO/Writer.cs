using _4.WildFarm.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.WildFarm.IO
{
    internal class Writer : IWriter
    {
        public void WriteLine(string text) => Console.WriteLine(text);
    }
}
