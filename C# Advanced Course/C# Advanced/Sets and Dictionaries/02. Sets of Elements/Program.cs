namespace _02._Sets_of_Elements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashSet<int> first = new HashSet<int>();
            HashSet<int> second = new HashSet<int>();

            int[] bothSetsCounts = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int firstSetLength = bothSetsCounts[0];
            int secondSetLength = bothSetsCounts[1];

            for (int i = 0; i < firstSetLength; i++)
            {
                int numberToAdd = int.Parse(Console.ReadLine());
                first.Add(numberToAdd);
            }
            for (int i = 0; i < secondSetLength; i++)
            {
                int numberToAdd = int.Parse(Console.ReadLine());
                second.Add(numberToAdd);
            }

            first.IntersectWith(second);
            
            Console.WriteLine(string.Join(" ", first));
        }
    }
}