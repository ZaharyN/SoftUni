using _03.Raiding.IO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Raiding.IO
{
    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string str) => Console.WriteLine(str);
    }
}
