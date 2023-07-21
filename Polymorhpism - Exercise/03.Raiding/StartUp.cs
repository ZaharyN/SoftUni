using _03.Raiding.Core;
using _03.Raiding.Core.Interfaces;
using _03.Raiding.IO;
using _03.Raiding.IO.Interfaces;

namespace _03.Raiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader, writer);

            engine.Run();
        }
    }
}