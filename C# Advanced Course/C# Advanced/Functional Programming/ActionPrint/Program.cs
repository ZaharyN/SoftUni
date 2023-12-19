using System.Threading.Channels;

namespace ActionPoint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action<string[]> printer = str =>
                Console.WriteLine(string.Join(Environment.NewLine, str));

            string[] input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            printer(input);
        }
    }
}
