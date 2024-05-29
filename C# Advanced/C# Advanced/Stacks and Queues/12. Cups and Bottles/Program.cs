namespace _12._Cups_and_Bottles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<int> cups = new Queue<int>(Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());
            

            Stack<int> bottles = new Stack<int>(Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());

            int litresWasted = 0;

            while (cups.Any() && bottles.Any())
            {
                if (bottles.Peek() - cups.Peek() >= 0)
                {
                    litresWasted += bottles.Pop() - cups.Dequeue();
                }
                else
                {
                    int currentCupCapacity = cups.Peek();

                    while (currentCupCapacity > 0)
                    {
                        currentCupCapacity -= bottles.Pop();
                    }
                    litresWasted += Math.Abs(currentCupCapacity);
                    cups.Dequeue();
                }
            }
            if (cups.Count > 0)
            {
                Console.WriteLine($"Cups: {string.Join(" ", cups)}");
                Console.WriteLine($"Wasted litters of water: {litresWasted}");
            }
            else
            {
                Console.WriteLine($"Bottles: {string.Join(" ", bottles)}");
                Console.WriteLine($"Wasted litters of water: {litresWasted}");
            }
        }
    }
}