namespace Functional_Programming_Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<string, int> parser = s => int.Parse(s);
            Func<int, bool> filter = n => n % 2 == 0;
            Func<int, int> selector = n => n;

            int[] input = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(parser)
                .OrderBy(selector)
                .ToArray();

            int[] sorted = input.Where(filter).ToArray();

            Console.WriteLine(string.Join(", ", sorted));
        }
    }
}
