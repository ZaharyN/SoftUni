using System.Data;

int[] dimensions = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int rows = dimensions[0];
int cols = dimensions[1];

string[,] matrix = new string[rows, cols];

for (int row = 0; row < rows; row++)
{
    string[] columns = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries);

    for (int col = 0; col < cols; col++)
    {
        matrix[row, col] = columns[col];
    }
}

string input = string.Empty;

while ((input = Console.ReadLine()) != "END")
{
    string[] command = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    if (isValid(command, rows, cols))
    {
        int currentRow = int.Parse(command[1]);
        int currentCol = int.Parse(command[2]);
        int rowToSwap = int.Parse(command[3]);
        int colToSwap = int.Parse(command[4]);

        string temp = matrix[currentRow, currentCol];
        matrix[currentRow, currentCol] = matrix[rowToSwap, colToSwap];
        matrix[rowToSwap, colToSwap] = temp;

        PrintMatrix(rows, cols, matrix);
    }
    else
    {
        Console.WriteLine("Invalid input!");
        continue;
    }
}

void PrintMatrix(int rows, int cols, string[,] matrix)
{
    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            Console.Write(matrix[row, col] + " ");
        }
        Console.WriteLine();
    }
}

static bool isValid(string[] command, int rows, int cols)
{
    if (command[0] != "swap")
    {
        return false;
    }
    else if (command.Length != 5) { return false; }
    else if (int.Parse(command[1]) < 0 || int.Parse(command[1]) >= rows) { return false; }
    else if (int.Parse(command[2]) < 0 || int.Parse(command[2]) >= cols) { return false; }
    else if (int.Parse(command[3]) < 0 || int.Parse(command[3]) >= rows) { return false; }
    else if (int.Parse(command[4]) < 0 || int.Parse(command[4]) >= cols) { return false; }
    else { return true; }
}

