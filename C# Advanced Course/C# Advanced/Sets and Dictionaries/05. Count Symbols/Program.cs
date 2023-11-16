namespace _05._Count_Symbols
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SortedDictionary<char, int> symbols = new SortedDictionary<char, int>();

            string text = Console.ReadLine();

            foreach (var ch in text)
            {
                if (!symbols.ContainsKey(ch))
                {
                    symbols.Add(ch, 0);
                }
                symbols[ch]++;
            }
            foreach (var item in symbols)
            {
                Console.WriteLine($"{item.Key}: {item.Value} time/s");
            }
        }
    }
}