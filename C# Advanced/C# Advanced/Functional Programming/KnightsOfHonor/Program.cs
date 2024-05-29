namespace KnightsOfHonor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action<string[], string> printer = (str, title) =>
            {
                foreach (string s in str)
                {
                    Console.WriteLine($"{title} {s}");
                }
            };

            string[] input = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string title = "Sir";

            printer(input, title);
        }
    }
}
