using System.Security;

namespace _06._Wardrobe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, int>> clothes =
                new Dictionary<string, Dictionary<string, int>>();

            int lines = int.Parse(Console.ReadLine());

            for (int i = 0; i < lines; i++)
            {
                string[] tokens = Console.ReadLine()
                    .Split(new string[] { ",", " -> " }, StringSplitOptions.RemoveEmptyEntries);

                string color = tokens[0];

                if (!clothes.ContainsKey(color))
                {
                    clothes.Add(color, new Dictionary<string, int>());
                }

                for (int j = 1; j < tokens.Length; j++)
                {
                    string currentWear = tokens[j];
                    if (!clothes[color].ContainsKey(currentWear))
                    {
                        clothes[color].Add(currentWear, 0);
                    }
                    clothes[color][currentWear]++;
                }
                
            }
            string[] searchedDressInfo = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string searchedColor = searchedDressInfo[0];
            string searchedDress = searchedDressInfo[1];
            bool isFound = false;

            foreach (var colors in clothes)
            {
                isFound = false;
                Console.WriteLine($"{colors.Key} clothes:");

                if(colors.Key == searchedColor)
                {
                    isFound = true;
                }

                foreach (var clothesPerColor in colors.Value)
                {
                    if(clothesPerColor.Key == searchedDress && isFound)
                    {
                        Console.WriteLine($"* {clothesPerColor.Key} - {clothesPerColor.Value} (found!)");
                    }
                    else
                    {
                        Console.WriteLine($"* {clothesPerColor.Key} - {clothesPerColor.Value}");
                    }
                }
            }
        }
    }
}