using System.Collections.Immutable;

namespace CountUppercaseWords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<string, bool> isUpper = s => s[0] == s.ToUpper()[0];

            string[] text = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string[] sorted = text.Where(isUpper).ToArray();

            Console.WriteLine(string.Join(Environment.NewLine, sorted));

        }
    }
}
