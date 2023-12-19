namespace AppliedArithmetics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<List<int>, string, List<int>> mathFunction = (numbers, operation)
                =>
            {
                if (operation == "add")
                {
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        numbers[i]++;
                    }
                }
                else if (operation == "multiply")
                {
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        numbers[i] *= 2;
                    }
                }
                else if (operation == "subtract")
                {
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        numbers[i]--;
                    }
                }
                else if (operation == "print")
                {
                    Console.WriteLine(string.Join(" ", numbers));
                }
                return numbers;
            };

            List<int> input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            string command = string.Empty;
            while ((command = Console.ReadLine()) != "end")
            {
                string operation = command;

                mathFunction(input, operation);
            }
        }
    }
}
