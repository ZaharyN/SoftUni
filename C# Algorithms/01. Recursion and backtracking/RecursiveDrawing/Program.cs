using System;
using System.Linq;

namespace Recursion_and_Backtracking
{
	public class Program
	{
		public static void Main(string[] args)
		{
			int length = int.Parse(Console.ReadLine());

			DrawFigure(length);
		}

		public static void DrawFigure(int length)
		{
			if (length <= 0)
			{
				return;
			}

			Console.WriteLine(new string('*', length));

			DrawFigure(--length);

			Console.WriteLine(new string('#', ++length));
		}
	}
}



