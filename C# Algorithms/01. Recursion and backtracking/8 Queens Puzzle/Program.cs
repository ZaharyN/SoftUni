using System;
using System.Linq;
using System.Collections.Generic;

namespace _8_Queens_Puzzle
{
	internal class Program
	{
		public static HashSet<int> occupiedRows = new HashSet<int>();
		public static HashSet<int> occupiedCols = new HashSet<int>();
		public static HashSet<int> leftDiagonal = new HashSet<int>();
		public static HashSet<int> rightDiagonal = new HashSet<int>();
		static void Main(string[] args)
		{
			char[,] chessBoard = new char[8, 8];

			for (int i = 0; i < chessBoard.GetLength(0); i++)
			{
				for (int j = 0; j < chessBoard.GetLength(1); j++)
				{
					chessBoard[i, j] = '-';
				}
			}

			GenerateQueens(chessBoard, 0, 0, 0);
		}

		public static void GenerateQueens(char[,] chessBoard, int row, int col, int queensCount)
		{
			if (row < 0 || row >= chessBoard.GetLength(0) || col < 0 || col >= chessBoard.GetLength(1))
			{
				return;
			}

			if (occupiedRows.Contains(row)
				|| occupiedCols.Contains(col)
				|| leftDiagonal.Contains(col - row)
				|| rightDiagonal.Contains(col + row))
			{
				GenerateQueens(chessBoard, row, col + 1, queensCount);
				return;
			}

			chessBoard[row, col] = '*';
			queensCount++;

			if (queensCount == 8)
			{
				PrintChessBoard(chessBoard);
			}
			occupiedRows.Add(row);
			occupiedCols.Add(col);
			leftDiagonal.Add(col - row);
			rightDiagonal.Add(col + row);

			GenerateQueens(chessBoard, row + 1, 0, queensCount);

			if (chessBoard[row,col] == '*')
			{
				chessBoard[row, col] = '-';
				queensCount--;
				occupiedRows.Remove(row);
				occupiedCols.Remove(col);
				leftDiagonal.Remove(col - row);
				rightDiagonal.Remove(col + row);
				GenerateQueens(chessBoard, row, col + 1, queensCount);
			}
		}

		private static void PrintChessBoard(char[,] chessBoard)
		{
			for (int i = 0; i < chessBoard.GetLength(0); i++)
			{
				for (int j = 0; j < chessBoard.GetLength(1); j++)
				{
					Console.Write(chessBoard[i, j] + " ");
				}
				Console.WriteLine();
			}
            Console.WriteLine();
        }
	}
}
