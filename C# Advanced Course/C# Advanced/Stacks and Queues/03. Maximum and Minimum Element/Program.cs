namespace _03._Maximum_and_Minimum_Element
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int queries = int.Parse(Console.ReadLine());

            Stack<int> numbers = new Stack<int>();

            for (int i = 0; i < queries; i++)
            {
                int[] querie = Console.ReadLine()
                    .Split()
                    .Select(int.Parse)
                    .ToArray();

                int operation = querie[0];

                if(operation == 1)
                {
                    numbers.Push(querie[1]);
                }
                else if(operation == 2)
                {
                    if (numbers.Any())
                    {
                        numbers.Pop();
                    }
                }
                else if(operation == 3)
                {
                    if (numbers.Any())
                    {
                        Console.WriteLine(numbers.Max());
                    }
                }
                else
                {
                    if (numbers.Any())
                    {
                        Console.WriteLine(numbers.Min());
                    }
                }
            }
            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}