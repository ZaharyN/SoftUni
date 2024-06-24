using System;
using System.Linq;
using System.Collections.Generic;

namespace Connected_Areas_in_Matrix
{
	public class Area
	{
        public int Size { get; set; }
		public int[] StartPositionCoordiantes { get; set; } = new int[2];
    }
	internal class Program
	{
		public static char[,] matrix;
		public static int areasFound = 0;
		public static int currentAreaSize = 0;
		public static List<Area> areas = new List<Area>();
		static void Main(string[] args)
		{
			int rows = int.Parse(Console.ReadLine());
			int cols = int.Parse(Console.ReadLine());

			matrix = new char[rows, cols];

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				string rowInput = Console.ReadLine();

				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					matrix[i, j] = rowInput[j];
				}
			}
			Area area = new();

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					currentAreaSize = 0;
					area.StartPositionCoordiantes = new[] { i, j };
					FindAreas(i, j);

					if(currentAreaSize > 0)
					{
						areasFound++;
						area.Size = currentAreaSize;
						areas.Add(area);
					}
				}
			}

            Console.WriteLine("Total areas found: " + areasFound);
			int counter = 1;

            foreach (var item in areas
				.OrderByDescending(x => x.Size)
					.ThenBy(x => x.StartPositionCoordiantes[0])
						.ThenBy(x => x.StartPositionCoordiantes[1]))
			{
                Console.WriteLine($"Area #{counter} at ({item.StartPositionCoordiantes[0]},{item.StartPositionCoordiantes[1]}), size: {item.Size}");
				counter++;
            }
		}

		private static void FindAreas(int row, int col)
		{
			if (IsOutside(row, col)
			|| IsWall(row, col)
			|| IsVisited(row, col))
			{
				return;
			}

			matrix[row, col] = 'v';
			currentAreaSize++;
			FindAreas(row - 1, col);
			FindAreas(row + 1, col);
			FindAreas(row, col + 1);
			FindAreas(row, col - 1);
		}

		private static bool IsOutside(int row, int col)
		{
			return row < 0
				|| col < 0
				|| row >= matrix.GetLength(0)
				|| col >= matrix.GetLength(1);
		}

		private static bool IsWall(int row, int col)
		{
			return matrix[row, col] == '*';
		}

		private static bool IsVisited(int row, int col)
		{
			return matrix[row, col] == 'v';
		}
	}
}
