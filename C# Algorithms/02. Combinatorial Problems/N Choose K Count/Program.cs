using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace N_Choose_K_Count
{
	internal class Program
	{
		public static int n;
		public static int k;
		static void Main(string[] args)
		{
			n = int.Parse(Console.ReadLine());
			k = int.Parse(Console.ReadLine());

			int result = 0;
			Console.WriteLine(GetBinom(n, k));
		}

		private static int GetBinom(int row, int col)
		{
			if (row == 0 || col == 0 || col == row)
			{
				return 1;
			}

			return GetBinom(row - 1, col - 1) + GetBinom(row - 1, col);
		}
	}
}
