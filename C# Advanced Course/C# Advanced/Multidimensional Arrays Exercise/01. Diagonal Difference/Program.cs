using System.ComponentModel.Design;

int size = int.Parse(Console.ReadLine());

int[,] matrix = new int[size, size];

for (int row = 0; row < matrix.GetLength(0); row++)
{
    int[] columns = Console.ReadLine()
        .Split()
        .Select(int.Parse)
        .ToArray();

    for (int col = 0; col < matrix.GetLength(1); col++)
    {
        matrix[row, col] = columns[col];
    }
}
int primaryDiagonalSum = 0;
int secondaryDiagonalSum = 0;

for (int row = 0; row < matrix.GetLength(0); row++)
{
    primaryDiagonalSum += matrix[row, row];
    secondaryDiagonalSum += matrix[matrix.GetLength(0) - 1 - row, row];
}

int absoluteDifference = Math.Abs(primaryDiagonalSum - secondaryDiagonalSum);
Console.WriteLine(absoluteDifference);