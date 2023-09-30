using _4.WildFarm.Core;
using _4.WildFarm.Core.Interfaces;
using _4.WildFarm.IO;
using _4.WildFarm.IO.Interfaces;

namespace _4.WildFarm
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new Reader();
            IWriter writer = new Writer();

            IEngine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}