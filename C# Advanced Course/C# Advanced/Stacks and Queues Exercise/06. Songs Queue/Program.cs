namespace _06._Songs_Queue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<string> songs = new (
                Console.ReadLine()
                .Split(", ", StringSplitOptions.RemoveEmptyEntries));

            while (songs.Any())
            {
                string[] cmdArguments = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string operation = cmdArguments[0];

                if(operation == "Play")
                {
                    songs.Dequeue();
                }
                else if(operation == "Add")
                {
                    string songToAdd = string.Join(" ", cmdArguments.Skip(1));

                    if (songs.Contains(songToAdd))
                    {
                        Console.WriteLine($"{songToAdd} is already contained!");
                        continue;
                    }
                    songs.Enqueue(songToAdd);
                }
                else if(operation == "Show")
                {
                    Console.WriteLine(string.Join(", ", songs));
                }
            }
            Console.WriteLine("No more songs!");
        }
    }
}