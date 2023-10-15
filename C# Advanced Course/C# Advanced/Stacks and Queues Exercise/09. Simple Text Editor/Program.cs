internal class Program
{
    private static void Main(string[] args)
    {
        int numberOfOperations = int.Parse(Console.ReadLine());

        Stack<string> changes = new Stack<string>();
        string text = string.Empty;

        for (int i = 0; i < numberOfOperations; i++)
        {
            string[] command = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string operation = command[0];

            if (operation == "1")
            {
                changes.Push(text);

                string textToAdd = command[1];
                text += textToAdd;
            }
            else if (operation == "2")
            {
                changes.Push(text);

                int countToErase = int.Parse(command[1]);
                text = text.Remove(text.Length - countToErase);
            }
            else if (operation == "3")
            {
                int index = int.Parse(command[1]) - 1;
                Console.WriteLine(text[index]);
            }
            else if (operation == "4")
            {
                text = changes.Pop();
            }
        }
    }
}