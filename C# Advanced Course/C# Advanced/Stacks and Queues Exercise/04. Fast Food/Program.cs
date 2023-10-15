using System.Threading.Channels;

namespace _04._Fast_Food
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int foodQuantity = int.Parse(Console.ReadLine());

            Queue<int> orders = new(
                Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());

            Console.WriteLine(orders.Max());

            while (orders.Any())
            {
                int nextOrder = orders.Peek();

                foodQuantity -= nextOrder;

                if(foodQuantity < 0)
                {
                    break;
                }

                orders.Dequeue();
            }
            if (orders.Any())
            {
                Console.WriteLine($"Orders left: {string.Join(" ", orders)}");
            }
            else
            {
                Console.WriteLine("Orders complete");
            }
        }
    }
}