namespace _07._Knight_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int size = int.Parse(Console.ReadLine());

            char[,] matrix = new char[size, size];

            for (int i = 0; i < size; i++)
            {
                string columns = Console.ReadLine();

                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = columns[j];
                }
            }
            //if (size <= 2)
            //{
            //    Console.WriteLine(0);
            //    { return; }
            //}

            int mostAttackingKnight = 0;
            int knightsRemoved = 0;


            while (true)
            {
                mostAttackingKnight = 0;
                int currentKnightAttacks = 0;
                int mostAttackingKnghtRow = 0;
                int mostAttackingKnghtCol = 0;

                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        if (matrix[row, col] == 'K')
                        {
                            int currentKnightRow = row;
                            int currentKnightCol = col;
                            currentKnightAttacks = 0;
                            //Up Knights 

                            int firstKnightUpRow = currentKnightRow - 2;
                            int firstKnightUpCol = currentKnightCol - 1;

                            if (areValid(firstKnightUpRow, firstKnightUpCol, matrix))
                            {
                                if (matrix[firstKnightUpRow, firstKnightUpCol] == 'K')
                                {
                                    currentKnightAttacks++;
                                }
                            }

                            int secondKnighUptRow = currentKnightRow - 2;
                            int secondKnightUpCol = currentKnightCol + 1;

                            if (areValid(secondKnighUptRow, secondKnightUpCol, matrix))
                            {
                                if (matrix[secondKnighUptRow, secondKnightUpCol] == 'K')
                                {
                                    currentKnightAttacks++;
                                }
                            }

                            // Left Knights

                            int firstKnightLeftRow = currentKnightRow - 1;
                            int firstKnightLeftCol = currentKnightCol - 2;

                            if (areValid(firstKnightLeftRow, firstKnightLeftCol, matrix))
                            {
                                if (matrix[firstKnightLeftRow, firstKnightLeftCol] == 'K')
                                {
                                    currentKnightAttacks++;
                                }
                            }

                            int secondKnighLefttRow = currentKnightRow + 1;
                            int secondKnightLeftCol = currentKnightCol - 2;

                            if (areValid(secondKnighLefttRow, secondKnightLeftCol, matrix))
                            {
                                if (matrix[secondKnighLefttRow, secondKnightLeftCol] == 'K')
                                {
                                    currentKnightAttacks++;
                                }
                            }
                            // Right Knights

                            int firstKnightRighttRow = currentKnightRow - 1;
                            int firstKnightRighttCol = currentKnightCol + 2;

                            if (areValid(firstKnightRighttRow, firstKnightRighttCol, matrix))
                            {
                                if (matrix[firstKnightRighttRow, firstKnightRighttCol] == 'K')
                                {
                                    currentKnightAttacks++;
                                }
                            }

                            int secondKnighRightRow = currentKnightRow + 1;
                            int secondKnighRightCol = currentKnightCol + 2;

                            if (areValid(secondKnighRightRow, secondKnighRightCol, matrix))
                            {
                                if (matrix[secondKnighRightRow, secondKnighRightCol] == 'K')
                                {
                                    currentKnightAttacks++;
                                }
                            }
                            // Down Knights

                            int firstKnightDowntRow = currentKnightRow + 2;
                            int firstKnightDowntCol = currentKnightCol - 1;

                            if (areValid(firstKnightDowntRow, firstKnightDowntCol, matrix))
                            {
                                if (matrix[firstKnightDowntRow, firstKnightDowntCol] == 'K')
                                {
                                    currentKnightAttacks++;
                                }
                            }

                            int secondKnightDownRow = currentKnightRow + 2;
                            int secondKnighDownCol = currentKnightCol + 1;

                            if (areValid(secondKnightDownRow, secondKnighDownCol, matrix))
                            {
                                if (matrix[secondKnightDownRow, secondKnighDownCol] == 'K')
                                {
                                    currentKnightAttacks++;
                                }
                            }

                            if (currentKnightAttacks > mostAttackingKnight && currentKnightAttacks > 0)
                            {
                                mostAttackingKnight = currentKnightAttacks;
                                mostAttackingKnghtRow = currentKnightRow;
                                mostAttackingKnghtCol = currentKnightCol;

                            }
                        }
                    }
                }
                if (mostAttackingKnight > 0)
                {
                    matrix[mostAttackingKnghtRow, mostAttackingKnghtCol] = '0';
                    knightsRemoved++;
                }
                else
                {
                    break;
                }

            }

            Console.WriteLine(knightsRemoved);

            static bool areValid(int row, int col, char[,] matrix)
            {
                if (row < 0 || col < 0)
                {
                    return false;
                }
                else if (row >= matrix.GetLength(0) || col >= matrix.GetLength(1))
                {
                    return false;
                }
                return true;
            }
        }
    }
}