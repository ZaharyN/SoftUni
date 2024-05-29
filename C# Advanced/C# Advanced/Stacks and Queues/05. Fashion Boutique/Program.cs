namespace _05._Fashion_Boutique
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack<int> clothes = new Stack<int>(Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray());

            int rackCapacity = int.Parse(Console.ReadLine());
            int currentRackCapacity = rackCapacity;
            int racksUsed = 1;

            while (clothes.Any())
            {
                currentRackCapacity -= clothes.Peek();

                if(currentRackCapacity < 0)
                {
                    currentRackCapacity = rackCapacity;
                    racksUsed++;
                    continue;
                }
                
                clothes.Pop();
            }
            Console.WriteLine(racksUsed);
        }
    }
}