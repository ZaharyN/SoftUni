namespace AddVAT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] input = Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .Select(x => x * 1.2)
                .ToArray();

            Array.ForEach(input, x => Console.WriteLine($"{x:f2}"));
        }
    }
}
