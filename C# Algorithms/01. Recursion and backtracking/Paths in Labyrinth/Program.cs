using System;
using System.Linq;
using System.Collections.Generic;

namespace Paths_in_Labyrinth
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<string> path = new List<string>();
			int rows = int.Parse(Console.ReadLine());
			int cols = int.Parse(Console.ReadLine());

			char[,] lab = new char[rows, cols];

			for (int i = 0; i < lab.GetLength(0); i++)
			{
				string column = Console.ReadLine();

				for (int j = 0; j < lab.GetLength(1); j++)
				{
					lab[i, j] = column[j];
				}
			}

			FindPath(lab, 0, 0, path, string.Empty);
		}

		public static void FindPath(char[,] lab, int row, int col, List<string> path, string direction)
		{
			if (row < 0 || row >= lab.GetLength(0) || col < 0 || col >= lab.GetLength(1))
			{
				return;
			}

			if (lab[row, col] == '*')
			{
				return;
			}

			if (lab[row, col] == 'e')
			{
				path.Add(direction);
				Console.WriteLine(string.Join("", path));
				path.RemoveAt(path.Count - 1);
				return;
			}

			if (lab[row, col] == '-' && lab[row, col] != 'M')
			{
				path.Add(direction);
				lab[row, col] = 'M';

				FindPath(lab, row - 1, col, path, "U");
				FindPath(lab, row, col + 1, path, "R");
				FindPath(lab, row, col - 1, path, "L");
				FindPath(lab, row + 1, col, path, "D");

				path.RemoveAt(path.Count - 1);
				lab[row, col] = '-';
			}
		}
	}
}
