using System;
using System.Linq;

namespace Recursion_and_Backtracking
{
	public class Program
	{
		public static void Main(string[] args)
		{
			int[] array = Console.ReadLine()
			.Split()
			.Select(int.Parse)
			.ToArray();

			Console.WriteLine(AddArrayIndex(array, 0));
		}
		public static int AddArrayIndex(int[] array, int index)
		{
			if (index >= array.Length)
			{
				return 0;
			}

			return array[index] + AddArrayIndex(array, index + 1);
		}
	}
}

