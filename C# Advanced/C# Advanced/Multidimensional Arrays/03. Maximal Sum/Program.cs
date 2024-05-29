int[] dimensions = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int rows = dimensions[0];
int cols = dimensions[1];

int[,] matrix = new int[rows, cols];

for (int row = 0; row < rows; row++)
{
    int[] columns = Console.ReadLine()
        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();

    for (int col = 0; col < cols; col++)
    {
        matrix[row, col] = columns[col];
    }
}

int matrix3x3biggestSum = 0;
int maxRow = 0;
int maxCol = 0;

for (int row = 0; row <= rows - 3; row++)
{
    int matrix3x3currentSum = 0;

    for (int col = 0; col <= cols - 3; col++)
    {
        matrix3x3currentSum += matrix[row, col] + matrix[row, col + 1] + matrix[row, col + 2]
            + matrix[row + 1, col] + matrix[row + 1, col + 1] + matrix[row + 1, col + 2]
            + matrix[row + 2, col] + matrix[row + 2, col + 1] + matrix[row + 2, col + 2];

        if (matrix3x3currentSum > matrix3x3biggestSum)
        {
            matrix3x3biggestSum = matrix3x3currentSum;
            maxRow = row;
            maxCol = col;
        }
        matrix3x3currentSum = 0;
    }
}
Console.WriteLine($"Sum = {matrix3x3biggestSum}");

for (int row = maxRow; row < maxRow + 3; row++)
{
    for (int col = maxCol; col < maxCol + 3; col++)
    {
        Console.Write(matrix[row, col] + " ");
    }
    Console.WriteLine();
}

