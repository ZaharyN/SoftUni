namespace CustomMinFunction
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<int[], int> minNumber = numbers =>
            {
                int min = int.MaxValue;

                foreach (int i in numbers)
                {
                    if (i < min)
                    {
                        min = i;
                    }
                }
                return min;
            };

            int[] input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Console.WriteLine(minNumber(input));
        }
    }
}
