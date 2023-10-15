namespace _03._Periodic_Table
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SortedSet<string> elements = new();

            int lines = int.Parse(Console.ReadLine());

            for (int i = 0; i < lines; i++)
            {
                string[] inputELements = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                elements.UnionWith(inputELements);
                //int inputLength = inputELements.Length;

                //for (int j = 0; j < inputLength; j++)
                //{
                //    elements.Add(inputELements[j]);
                //}
            }
            Console.Write(string.Join(" ", elements) + " ");
        }
    }
}