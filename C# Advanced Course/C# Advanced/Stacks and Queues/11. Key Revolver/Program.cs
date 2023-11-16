namespace _11._Key_Revolver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int singleBulletPrice = int.Parse(Console.ReadLine());
            int gunBarrelSize = int.Parse(Console.ReadLine());

            Stack<int> bullets = new Stack<int>(Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());

            Queue<int> locks = new Queue<int>(Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());

            int intelligence = int.Parse(Console.ReadLine());
            int bulletsFired = 0;
            int locksUnlocked = 0;
            int workingGunBarrelSize = gunBarrelSize;

            while (bullets.Any() && locks.Any())
            {
                if (bullets.Peek() > locks.Peek())
                {
                    Console.WriteLine("Ping!");
                    bullets.Pop();
                    bulletsFired++;
                    workingGunBarrelSize--;

                    if (workingGunBarrelSize == 0 && bullets.Count > 0)
                    {
                        workingGunBarrelSize = gunBarrelSize;
                        Console.WriteLine("Reloading!");
                    }
                }
                else
                {
                    Console.WriteLine("Bang!");
                    bullets.Pop();
                    locks.Dequeue();
                    bulletsFired++;
                    locksUnlocked++;
                    workingGunBarrelSize--;

                    if (workingGunBarrelSize == 0 && bullets.Count > 0)
                    {
                        workingGunBarrelSize = gunBarrelSize;
                        Console.WriteLine("Reloading!");
                    }
                }
            }
            int moneySpent = bulletsFired * singleBulletPrice;

            if (locks.Count == 0)
            {
                Console.WriteLine($"{bullets.Count} bullets left. Earned ${intelligence - moneySpent}");
            }
            else if (bullets.Count == 0)
            {
                Console.WriteLine($"Couldn't get through. Locks left: {locks.Count}");
            }

        }
    }
}