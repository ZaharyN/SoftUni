int rows = int.Parse(Console.ReadLine());

int[][] jagged = new int[rows][];

for (int row = 0; row < rows; row++)
{
    jagged[row] = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();
}

for (int row = 0; row < rows - 1; row++)
{
    for (int col = 0; col < jagged[row].Length; col++)
    {
        if (jagged[row].Length == jagged[row + 1].Length)
        {
            jagged[row][col] *= 2;
            jagged[row + 1][col] *= 2;
        }
        else
        {
            int currentRowLength = jagged[row].Length;
            int belowColLength = jagged[row + 1].Length;

            if (currentRowLength > belowColLength)
            {
                if (col < belowColLength)
                {
                    jagged[row][col] /= 2;
                    jagged[row + 1][col] /= 2;
                }
                else
                {
                    jagged[row][col] /= 2;
                }
            }
            else if (belowColLength > currentRowLength)
            {
                if (col < currentRowLength)
                {
                    jagged[row][col] /= 2;
                    jagged[row + 1][col] /= 2;
                }
                if (col + 1 == currentRowLength)
                {
                    for (int i = col + 1; i < belowColLength; i++)
                    {
                        jagged[row + 1][i] /= 2;
                    }
                }
            }
        }
    }
}

//foreach (var item in jagged)
//{
//    Console.WriteLine(string.Join(" ", item));
//}

string input = string.Empty;

while ((input = Console.ReadLine()) != "End")
{
    string[] command = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    string operation = command[0];

    if (operation == "Add")
    {
        int rowToAdd = int.Parse(command[1]);
        int colToAdd = int.Parse(command[2]);
        int value = int.Parse(command[3]);

        if (IsValid(rowToAdd, colToAdd, jagged))
        {
            jagged[rowToAdd][colToAdd] += value;
        }

    }
    else if (operation == "Subtract")
    {
        int rowToSubtract = int.Parse(command[1]);
        int colToSubtract = int.Parse(command[2]);
        int value = int.Parse(command[3]);

        if (IsValid(rowToSubtract, colToSubtract, jagged))
        {
            jagged[rowToSubtract][colToSubtract] -= value;
        }
    }

}
foreach (var items in jagged)
{
    Console.WriteLine(string.Join(" ", items));
}

static bool IsValid(int row, int col, int[][] jagged)
{
    if (row < 0 || col < 0) { return false; }
    if (row >= jagged.GetLength(0)) { return false; }
    if (col >= jagged[row].Length) { return false; }
    return true;

}