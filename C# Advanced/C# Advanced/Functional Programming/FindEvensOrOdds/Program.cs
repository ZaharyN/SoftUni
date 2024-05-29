namespace FindEvensOrOdds
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Predicate<int> isEvenMatch = x => x % 2 == 0;
            Predicate<int> isOdd = x => x % 2 != 0;

            List<int> list = new List<int>();

            int[] ints = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            string condition = Console.ReadLine();

            for (int i = ints[0]; i <= ints[1]; i++)
            {
                if (condition == "even" && isEvenMatch(i))
                {
                    list.Add(i);
                }
                else if (condition == "odd" && isOdd(i))
                {
                    list.Add(i);
                }
            }
            if (list.Count > 0)
            {
                Console.WriteLine(string.Join(" ", list));
            }
        }
    }
}
