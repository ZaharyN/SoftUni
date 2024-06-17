using System;

namespace _02_
{
	internal class Program
	{
		public static int n;
		public static int[] variations;
		static void Main(string[] args)
		{
			n = int.Parse(Console.ReadLine());

			variations = new int[n];
			GenerateVectors(0);
		}

		private static void GenerateVectors(int index)
		{
			if(index >= n)
			{
                Console.WriteLine(string.Join(" ", variations));
                return;
			}

			for (int i = 1; i <= n; i++)
			{
				variations[index] = i;
				GenerateVectors(index + 1);
			}
		}
	}
}
