using System.Numerics;

int[] dimensions = Console.ReadLine()
    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
    .Select(int.Parse)
    .ToArray();

int rows = dimensions[0];
int cols = dimensions[1];

char[,] isle = new char[rows, cols];
string snake = Console.ReadLine();

int snakeIndex = 0;
int snakeLength = snake.Length;

for (int row = 0; row < rows; row++)
{
    if (row % 2 == 0)
    {
        for (int col = 0; col < cols; col++)
        {
            isle[row, col] = snake[snakeIndex];
            snakeIndex++;

            if (snakeIndex == snakeLength)
            {
                snakeIndex = 0;
            }
        }
    }
    else if (row % 2 != 0)
    {
        for (int col = cols - 1; col >= 0; col--)
        {
            isle[row, col] = snake[snakeIndex];
            snakeIndex++;

            if (snakeIndex == snakeLength)
            {
                snakeIndex = 0;
            }
        }
    }
}

for (int row = 0; row < rows; row++)
{
    for (int col = 0; col < cols; col++)
    {
        Console.Write(isle[row, col]);
    }
    Console.WriteLine();
}

