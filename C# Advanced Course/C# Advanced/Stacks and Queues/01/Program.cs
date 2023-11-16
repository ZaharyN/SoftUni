namespace _01_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] cmdArguments = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int elementsToPush = cmdArguments[0];
            int elementsToPop = cmdArguments[1];
            int elementToLookFor = cmdArguments[2];

            Stack<int> stack = new Stack<int> 
                (Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray());

            for (int i = 0; i < elementsToPop; i++)
            {
                stack.Pop();
            }

            if (stack.Contains(elementToLookFor))
            {
                Console.WriteLine("true");
            }
            else
            {
                if (stack.Any())
                {
                    Console.WriteLine(stack.Min());
                }
                else
                {
                    Console.WriteLine(0);
                }
            }
        }
    }
}