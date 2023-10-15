using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace _07._The_V_Logger
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, SortedSet<string>>> vLoggers =
                new Dictionary<string, Dictionary<string, SortedSet<string>>>();

            string input = string.Empty;

            while ((input = Console.ReadLine()) != "Statistics")
            {
                string[] command = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = command[0];
                string operation = command[1];

                if (operation == "joined")
                {
                    if (vLoggers.ContainsKey(name))
                    {
                        continue;
                    }

                    vLoggers.Add(name, new Dictionary<string, SortedSet<string>>());
                    vLoggers[name].Add("followers", new SortedSet<string>());
                    vLoggers[name].Add("following", new SortedSet<string>());
                }
                else if (operation == "followed")
                {
                    string followedVlogger = command[2];

                    if (!vLoggers.ContainsKey(name) || !vLoggers.ContainsKey(followedVlogger) || name == followedVlogger)
                    {
                        continue;
                    }

                    vLoggers[name]["following"].Add(followedVlogger);
                    vLoggers[followedVlogger]["followers"].Add(name);
                }
            }
            int counter = 1;

            Dictionary<string, Dictionary<string, SortedSet<string>>> ordered =
                vLoggers.OrderByDescending(x => x.Value["followers"].Count)
                        .ThenBy(y => y.Value["following"].Count)
                        .ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine($"The V-Logger has a total of {vLoggers.Count} vloggers in its logs.");

            foreach (var item in ordered)
            {
                Console.WriteLine($"{counter}. {item.Key} : {item.Value["followers"].Count} followers, {item.Value["following"].Count} following");
                if(counter == 1)
                {
                    foreach (var follower in item.Value["followers"])
                    {
                        Console.WriteLine($"*  {follower}");
                    }
                }
                counter++;
            }
        }
    }
}